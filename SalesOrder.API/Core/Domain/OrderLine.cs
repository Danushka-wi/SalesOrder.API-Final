using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class OrderLine:Entity
    {
        public string Note { get; private set; }
        public Quantity Quantity { get; private set; }
        public Tax Tax { get; private set; }
        public decimal ExclAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal InclAmount { get; private set; }
        public virtual Item Item { get; }
        public virtual Order Order { get; }

        protected OrderLine()
        {

        }

        public OrderLine(string note, Quantity quantity, Tax tax, Item item, Order order)
        {
            Note = note;
            Quantity = quantity;
            Tax = tax;
            Item = item;
            Order = order;
        }

        public void UpdateOrderLine(string note, Quantity quantity, Tax tax)
        {
            Note = note;
            Quantity = quantity;
            Tax = tax;
        }

        public void SetOrderLineTotals()
        {
            this.ExclAmount = this.Quantity * this.Item.Price;
            this.TaxAmount = this.ExclAmount * (this.Tax.Value / 100);
            this.InclAmount = this.ExclAmount + this.TaxAmount;
        }
    }
}
