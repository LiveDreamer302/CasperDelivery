using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Specification;

namespace CasperDelivery.Interfaces.Specifications
{
    public class GetBasketWithItemsSpecification : BaseSpecification<BasketItem>
    {
        public GetBasketWithItemsSpecification(int Basketid) : base(x => x.Id == Basketid)
        {
            AddInclude(x => x.Basket);
            AddInclude(x => x.Product);
        }
    }
}
