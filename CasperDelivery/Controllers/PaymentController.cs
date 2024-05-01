using System.Security.Claims;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Payment;
using CasperDelivery.Interfaces.Repositories;
using CasperDelivery.Interfaces.Specifications;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;

namespace CasperDelivery.Controllers;

[Route("[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{   
    private readonly IGenericRepository<BasketItem> _basketItemRepo;
    private readonly ICartRepository _basketRepo;
    private readonly IGenericRepository<Orders> _orderRepo;
    private readonly IPaymentService _paymentService;
    private readonly IConfiguration _config;

    public PaymentController(IGenericRepository<BasketItem> basketItemRepo, ICartRepository basketRepo, 
                            IGenericRepository<Orders> orderRepo, IPaymentService paymentService, IConfiguration config)
    {
        _basketItemRepo = basketItemRepo;
        _basketRepo = basketRepo;
        _orderRepo = orderRepo;
        _paymentService = paymentService;
        _config = config;
    }

    [HttpPost("checkout")]
    public ActionResult CreateCheckoutSession(CheckoutPostData data)
    {
        var session = _paymentService.CreateCheckoutSession(data.Items, data.UserId);
        return Ok(session.Url);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Index()
    {
        string WhSecret = _config["StripeWh"];
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], WhSecret, throwOnApiVersionMismatch: false);
            Orders order = new Orders();
            IReadOnlyList<BasketItem> _basket;
            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var paymentIntent = (Session)stripeEvent.Data.Object;
                var userId = paymentIntent.Metadata["UserId"];
                var basket = await _basketRepo.GetBasketByUserId(userId);
                var spec = new GetBasketWithItemsSpecification(basket.Id);
                _basket = await _basketItemRepo.ListAsync(spec);

                order = new Orders
                {
                    Date = DateTime.Now,
                    UserId = userId,
                    TotalPrice = _basket.Sum(x => x.Product.Price * x.Quantity) + 5
                };

                var orderItems = _basket.Select(bi => new OrderItem
                {
                    ProductId = bi.ProductId,
                    Quantity = bi.Quantity
                }).ToList();
                order.Items = orderItems;
                foreach (var bi in _basket)
                {
                    await _basketItemRepo.DeleteAsync(bi.Id);
                }
                
                await _orderRepo.CreateAsync(order);
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return Ok();
        }
    }
}
