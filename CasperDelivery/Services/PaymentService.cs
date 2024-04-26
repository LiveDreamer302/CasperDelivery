using System.Drawing;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Payment;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;


namespace CasperDelivery.Services;

public class PaymentService : IPaymentService
{
    public PaymentService(IConfiguration configuration)
    {
        StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
    }
    public Session CreateCheckoutSession(List<BasketItem> cartItems, string userId)
    {
        var lineItems = new List<SessionLineItemOptions>();
        cartItems.ForEach(ci => lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions{
            UnitAmountDecimal = ci.Product.Price * 100,
            Currency = "usd",
            ProductData = new SessionLineItemPriceDataProductDataOptions
            {
                Name = ci.Product.Name,
                Images = new List<string>{ci.Product.PictureUrl}
            }
        },
        Quantity = ci.Quantity
        }));
        lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = 500,
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "Delivery"
                }
            },
            Quantity = 1
        });
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "https://localhost:5001/payment/success",
            CancelUrl = "https://localhost:5001",
            Metadata = new Dictionary<string, string>
            {
                { "UserId", userId }
            }
        };

        var service = new SessionService();
        Session session = service.Create(options);
        return session;
    }
}