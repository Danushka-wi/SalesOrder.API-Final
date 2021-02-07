using SalesOrder.API.Core.Domain;
using SalesOrder.API.Core.Repositories;
using SalesOrder.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(SalesOrderDBContext context)
            : base(context)
        {
        }

        public Order GetOrderWithOrderLines(long orderId)
        {
            Order order = SalesOrderDBContext.Orders.Find(orderId);
            if (order == null)
                return null;

            SalesOrderDBContext.Entry(order).Collection(x => x.OrderLines).Load();

            return order;
        }

        public SalesOrderDBContext SalesOrderDBContext
        {
            get { return Context as SalesOrderDBContext; }
        }
    }
}
