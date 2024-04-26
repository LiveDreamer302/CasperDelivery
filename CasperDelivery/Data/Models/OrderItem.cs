using System.Text.Json.Serialization;
using EllipticCurve.Utils;

namespace CasperDelivery.Data.Models
{
    public class OrderItem : BaseEntity
    {
        [JsonIgnore]
        public Orders Order { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}
