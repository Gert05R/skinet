using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    //We need to tell storexontext that there ae confugrations to look for
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            //Type is decimal, number of decimal places is set to two (sqlite does not support decimals, but in mysql in prod it does))
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            //Each product has a single brand(hasone), each brand can have many products(withmany)
            builder.HasOne(b => b.ProductBrand).WithMany()
                //entitiy framework builds this automatically, this just example of doing this manually
                .HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(b => b.ProductType).WithMany()
                .HasForeignKey(p => p.ProductTypeId);
        }
    }
}