using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Dtos
{
    public class ItemDto
    {
        public long Id { get; set; }
        public string Desription { get; set; }
        public decimal Price { get; set; }
    }
}
