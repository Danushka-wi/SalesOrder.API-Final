using SalesOrder.API.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderWithOrderLines(long orderId);
    }
}
