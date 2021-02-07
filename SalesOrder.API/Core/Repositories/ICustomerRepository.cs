using SalesOrder.API.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
