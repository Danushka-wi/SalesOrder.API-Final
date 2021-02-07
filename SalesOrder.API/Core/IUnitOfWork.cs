using SalesOrder.API.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IItemRepository Items { get; }
        IOrderRepository Orders { get; }
        int Complete();
    }
}
