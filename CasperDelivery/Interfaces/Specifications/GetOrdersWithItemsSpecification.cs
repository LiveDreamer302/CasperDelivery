using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Specification;

namespace CasperDelivery.Interfaces.Specifications;

public class GetOrdersWithItemsSpecification : BaseSpecification<OrderItem>
{
    public GetOrdersWithItemsSpecification(int orderId) : base(x => x.OrderId == orderId)
    {
        AddInclude(x => x.Order);
        AddInclude(x => x.Product);
    }
}