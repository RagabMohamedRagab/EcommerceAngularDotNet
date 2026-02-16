using Ecom.core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Configurations.FluentValidations.ProductFluentValidation
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(250);
            builder.Property(b => b.Description).HasMaxLength(500);
            builder.Property(b => b.NewPrice).HasPrecision(18, 2);
            builder.Property(b => b.OldPrice).HasPrecision(18, 2);
            builder.Property(b => b.Quantity).IsRequired();
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Category).WithMany(b => b.Products).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
