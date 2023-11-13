using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndOrderingSpecification(string email) : base(order => order.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems); // Include it
            AddInclude(o => o.DeliveryMethod); // Include it
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderWithItemsAndOrderingSpecification(int id, string email) 
        : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems); // Include it
            AddInclude(o => o.DeliveryMethod); // Include it
        }
    }
}