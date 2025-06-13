using DomainLayer.Models;
using E_Commerce.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                   .WithMany(/*b =>b.Products*/)
                   .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                  .WithMany(/*b =>b.Products*/)
                  .HasForeignKey(p => p.TypeId);

            builder.Property(p=>p.Price)
                   .HasColumnType("decimal(10,2)");
        }
    }
}
