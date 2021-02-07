using SalesOrder.API.Core;
using SalesOrder.API.Core.Repositories;
using SalesOrder.API.Data;
using SalesOrder.API.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesOrderDBContext _context;

        public UnitOfWork(SalesOrderDBContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Items = new ItemRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public ICustomerRepository Customers { get; private set; }

        public IItemRepository Items { get; private set; }


        public IOrderRepository Orders { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
