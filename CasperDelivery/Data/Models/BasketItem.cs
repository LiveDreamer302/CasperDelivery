
namespace CasperDelivery.Data.Models
{
    public class BasketItem : BaseEntity
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}