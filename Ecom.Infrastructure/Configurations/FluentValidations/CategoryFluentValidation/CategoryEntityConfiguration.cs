using Ecom.core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Configurations.FluentValidations.CategoryFluentValidation
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(250);
            builder.Property(b => b.Description).HasMaxLength(500);

            builder.HasKey(b => b.Id);

            builder.HasMany(b => b.Products).WithOne(b => b.Category).HasForeignKey(b => b.CategoryId).HasPrincipalKey(b => b.Id);
        }
    }
}
