using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prueba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.OrderDetailId);
            builder
                .HasOne(x => x.Order)
                .WithMany(y => y.OrderDetail)
                .HasForeignKey(z => z.OrderId)
                .IsRequired();
            builder
                .HasOne(x => x.Product)
                .WithMany(y => y.OrderDetail)
                .HasForeignKey(z => z.ProductId)
                .IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Subtotal).IsRequired();
        }
    }
}
