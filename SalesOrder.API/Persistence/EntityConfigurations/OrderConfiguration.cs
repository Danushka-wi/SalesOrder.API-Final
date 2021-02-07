using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrder.API.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.API.Persistence.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.Property(b => b.Id).HasColumnName("OrderID");

            orderConfiguration.Property(b => b.InvoiceNo).HasConversion(p => p.Value, p => InvoiceNo.Create(p).Value).IsRequired().HasMaxLength(10);
            orderConfiguration.Property(b => b.InvoiceDate).HasConversion(p => p.Value, p => InvoiceDate.Create(p).Value).IsRequired();
            orderConfiguration.Property(b => b.ReferenceNo).HasConversion(p => p.Value, p => ReferenceNo.Create(p).Value).IsRequired().HasMaxLength(10);

            orderConfiguration.HasOne(s => s.Customer).WithMany().IsRequired();
            orderConfiguration.HasMany(s => s.OrderLines).WithOne(e => e.Order);
        }
    }
}
