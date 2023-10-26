namespace CasperDelivery.Data.Models;

public class Products : BaseEntity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public Restaurants Restaurant { get; set; }
}