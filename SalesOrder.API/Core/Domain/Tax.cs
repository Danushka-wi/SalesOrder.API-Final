using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class Tax : ValueObject<Tax>
    {
        public decimal Value { get; }

        private Tax(decimal value)
        {
            Value = value;
        }

        public static Result<Tax> Create(decimal tax)
        {
            if (tax < 0)
                return Result.Fail<Tax>("Tax must be greater than 0");

            return Result.Ok(new Tax(tax));
        }

        protected override bool EqualsCore(Tax other)
        {
            return Value == other;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Tax tax)
        {
            return tax.Value;
        }
    }
}
