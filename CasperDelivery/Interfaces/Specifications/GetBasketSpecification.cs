using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Specification;

namespace CasperDelivery.Interfaces.Specifications
{
    public class GetBasketSpecification : BaseSpecification<Basket>
    {
        public GetBasketSpecification(string userId) : base(x => x.UserId == userId)
        {
            AddInclude(x => x.Items);
        }
    }
}
