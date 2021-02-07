using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class Customer : Entity
    {
        public string CustomerName { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string Suburb { get; private set; }
        public string State { get; private set; }
        public string PostCode { get; private set; }

        protected Customer()
        {

        }
        protected Customer(long id)
            : base(id)
        {
        }
    }
}
