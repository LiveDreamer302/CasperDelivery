namespace CasperDelivery.Data.Models;

public class Orders : BaseEntity
{
    public DateTime Date { get; set; }
    public AppUser User { get; set; }
    public List<Products> ProductsOrdered { get; set; }
    public double TotalPrice { get; set; }
}