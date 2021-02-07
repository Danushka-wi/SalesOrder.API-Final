using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class Quantity : ValueObject<Quantity>
    {
        public int Value { get; }

        private Quantity(int value)
        {
            Value = value;
        }

        public static Result<Quantity> Create(int quantity)
        {
            if (quantity < 0)
                return Result.Fail<Quantity>("Quantity must be greater than 0");

            return Result.Ok(new Quantity(quantity));
        }

        protected override bool EqualsCore(Quantity other)
        {
            return Value == other;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator int(Quantity quantity)
        {
            return quantity.Value;
        }
    }
}
