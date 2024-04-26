using CasperDelivery.Data.Models;
using Stripe.Checkout;

namespace CasperDelivery.Interfaces.Payment;

public interface IPaymentService
{
    Session CreateCheckoutSession(List<BasketItem> basketItems, string userId);
}