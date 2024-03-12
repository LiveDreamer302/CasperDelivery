namespace CasperDelivery.Data.Models
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
