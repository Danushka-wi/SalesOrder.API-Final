using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class InvoiceDate : ValueObject<InvoiceDate>
    {
        public DateTime Value { get; }

        private InvoiceDate(DateTime value)
        {
            Value = value;
        }

        public static Result<InvoiceDate> Create(DateTime invDate)
        {
            if (invDate > DateTime.Now)
                return Result.Fail<InvoiceDate>("Invoice Date should not be a future date");

            return Result.Ok(new InvoiceDate(invDate));
        }

        protected override bool EqualsCore(InvoiceDate other)
        {
            return Value == other;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator DateTime(InvoiceDate invDate)
        {
            return invDate.Value;

        }
    }
}
