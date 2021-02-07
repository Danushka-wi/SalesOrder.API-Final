using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class Item:Entity
    {
        public string Desription { get; private set; }
        public decimal Price { get; private set; }

        protected Item()
        {

        }
    }
}
