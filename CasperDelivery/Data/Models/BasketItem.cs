
using System.Text.Json.Serialization;

namespace CasperDelivery.Data.Models
{
    public class BasketItem : BaseEntity
    {
        [JsonIgnore]
        public Basket Basket { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}