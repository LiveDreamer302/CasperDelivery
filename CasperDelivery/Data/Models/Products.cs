using System.Text.Json.Serialization;

namespace CasperDelivery.Data.Models;

public class Products : BaseEntity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public int RestaurantId { get; set; }
    [JsonIgnore]
    public Restaurants Restaurant { get; set; }
    public List<Orders> Orders { get; set; }
}