using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Payment;
using CasperDelivery.Interfaces.Repositories;
using CasperDelivery.Interfaces.Specifications;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace CasperDelivery.Controllers;

[Route("[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IGenericRepository<BasketItem> _basketItemRepo;
    private readonly IPaymentService _paymentService;

    public PaymentController(IGenericRepository<BasketItem> basketItemRepo, IPaymentService paymentService)
    {
        _basketItemRepo = basketItemRepo;
        _paymentService = paymentService;
    }

    [HttpPost("checkout")]
    public ActionResult CreateCheckoutSession(List<BasketItem> cartItems)
    {
        var session = _paymentService.CreateCheckoutSession(cartItems);
        return Ok(session.Url);
    }
}
