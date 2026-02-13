using Ecom.core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Configurations.FluentValidations.ProductPhotoFluentValidtion
{
    public class ProductPhotoEntityConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
             builder.Property(b => b.ImageName).IsRequired().HasMaxLength(250);
                builder.HasOne(b => b.Product).WithMany(p => p.ProductPhotos).HasForeignKey(b => b.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
