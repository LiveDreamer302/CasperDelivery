namespace CasperDelivery.Data.Models;

public class Restaurants : BaseEntity
{
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public List<Products> Products { get; set; }
}