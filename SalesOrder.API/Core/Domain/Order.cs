using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class Order : Entity
    {
        public InvoiceNo InvoiceNo { get; }
        public InvoiceDate InvoiceDate { get; private set; }
        public ReferenceNo ReferenceNo { get; private set; }
        public string Note { get; private set; }
        public virtual Customer Customer { get; private set; }

        private readonly List<OrderLine> _orderLines = new List<OrderLine>();
        public virtual IReadOnlyList<OrderLine> OrderLines => _orderLines;

        protected Order()
        {

        }

        public Order(InvoiceNo invoiceNo, InvoiceDate invoiceDate, ReferenceNo referenceNo, string note, Customer customer)
        {
            InvoiceNo = invoiceNo;
            InvoiceDate = invoiceDate;
            ReferenceNo = referenceNo;
            Note = note;
            Customer = customer;
        }

        public void UpdateOrder(InvoiceDate invoiceDate, ReferenceNo referenceNo, string note, Customer customer)
        {
            InvoiceDate = invoiceDate;
            ReferenceNo = referenceNo;
            Note = note;
            Customer = customer;
        }

        public void AddOrderLine(string note, Quantity quantity, Tax tax, Item item, Order order)
        {
            var orderLine = new OrderLine(note, quantity, tax, item, this);

            orderLine.SetOrderLineTotals();

            _orderLines.Add(orderLine);
        }

        public void UpdateOrderLine(long orderLineId, string note, Quantity quantity, Tax tax)
        {
            OrderLine orderLine = _orderLines.FirstOrDefault(x => x.Id == orderLineId);

            if (orderLine == null)
                return;

            orderLine.UpdateOrderLine(note, quantity, tax);
            orderLine.SetOrderLineTotals();
        }

        public void RemoveOrderLine(long orderLineId)
        {
            OrderLine orderLine = _orderLines.FirstOrDefault(x => x.Id == orderLineId);

            if (orderLine == null)
                return;

            _orderLines.Remove(orderLine);
        }
    }
}
