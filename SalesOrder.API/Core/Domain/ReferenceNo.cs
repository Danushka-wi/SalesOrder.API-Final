using SalesOrder.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Core.Domain
{
    public class ReferenceNo : ValueObject<ReferenceNo>
    {
        public string Value { get; }

        private ReferenceNo(string value)
        {
            Value = value;
        }

        public static Result<ReferenceNo> Create(string refNo)
        {
            if (string.IsNullOrWhiteSpace(refNo))
                return Result.Fail<ReferenceNo>("Reference No should not be empty");

            refNo = refNo.Trim();

            if (refNo.Length > 10)
                return Result.Fail<ReferenceNo>("Reference No is too long");

            return Result.Ok(new ReferenceNo(refNo));
        }

        protected override bool EqualsCore(ReferenceNo other)
        {
            return Value == other;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(ReferenceNo refNo)
        {
            return refNo.Value;
        }
    }
}
