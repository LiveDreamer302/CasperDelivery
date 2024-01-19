using System.Linq.Expressions;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Specification;

namespace CasperDelivery.Interfaces.Specifications;

public class GetRestaurantWithProductSpecification : BaseSpecification<Restaurants>
{
    public GetRestaurantWithProductSpecification(int id ) : base(x => x.Id == id)
    {
        AddInclude(x => x.Products);
    }

    public GetRestaurantWithProductSpecification() : base() 
    {
        AddInclude(x => x.Products);
    }
}