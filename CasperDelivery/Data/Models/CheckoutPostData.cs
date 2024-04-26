namespace CasperDelivery.Data.Models;

public class CheckoutPostData
{
    public string UserId { get; set; }
    public List<BasketItem> Items { get; set; }
}