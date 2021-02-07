using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrder.API.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Persistence.EntityConfigurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> orderLineConfiguration)
        {
            orderLineConfiguration.Property(b => b.Id).HasColumnName("OrderLineID");

            orderLineConfiguration.Property(b => b.Quantity).HasConversion(p => p.Value, p => Quantity.Create(p).Value).IsRequired();
            orderLineConfiguration.Property(b => b.Tax).HasConversion(p => p.Value, p => Tax.Create(p).Value).IsRequired();

            orderLineConfiguration.HasOne(s => s.Item).WithMany().IsRequired();
            orderLineConfiguration.HasOne(s => s.Order).WithMany(s=>s.OrderLines).IsRequired();
        }
    }
}
