using SalesOrder.API.Core.Domain;
using SalesOrder.API.Core.Repositories;
using SalesOrder.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SalesOrderDBContext context)
            : base(context)
        {
        }
    }
}
