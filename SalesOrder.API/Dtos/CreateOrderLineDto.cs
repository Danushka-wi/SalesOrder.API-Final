using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Dtos
{
    public class CreateOrderLineDto
    {
        public long Id { get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }
        public decimal Tax { get; set; }
        public long ItemId { get; set; }
    }
}
