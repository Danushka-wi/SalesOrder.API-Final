using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class InvoiceNo : ValueObject<InvoiceNo>
    {
        public string Value { get; }

        private InvoiceNo(string value)
        {
            Value = value;
        }

        public static Result<InvoiceNo> Create(string invNo)
        {
            if (string.IsNullOrWhiteSpace(invNo))
                return Result.Fail<InvoiceNo>("Invoice No should not be empty");

            invNo = invNo.Trim();

            if (invNo.Length > 10)
                return Result.Fail<InvoiceNo>("Invoice No is too long");

            return Result.Ok(new InvoiceNo(invNo));
        }

        protected override bool EqualsCore(InvoiceNo other)
        {
            return Value == other;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(InvoiceNo invNo)
        {
            return invNo.Value;
        }
    }
}
