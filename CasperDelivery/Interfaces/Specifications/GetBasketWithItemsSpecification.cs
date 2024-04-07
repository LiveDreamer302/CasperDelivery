using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Specification;

namespace CasperDelivery.Interfaces.Specifications
{
    public class GetBasketWithItemsSpecification : BaseSpecification<BasketItem>
    {
        public GetBasketWithItemsSpecification(string userId) : base(x => x.UserId == userId)
        {
            AddInclude(x => x.Items);
            AddInclude(x => x.);            
        }
    }
}
