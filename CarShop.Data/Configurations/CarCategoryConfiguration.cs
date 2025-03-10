using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class CarCategoryConfiguration : IEntityTypeConfiguration<CarCategory>
    {
        public void Configure(EntityTypeBuilder<CarCategory> builder)
        {

            builder.HasKey(cc => new { cc.CarId, cc.CategoryId });

            // Car ile ilişki
            builder.HasOne(cc => cc.Car)
                .WithMany(c => c.CarCategories)
                .HasForeignKey(cc => cc.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category ile ilişki
            builder.HasOne(cc => cc.Category)
                .WithMany(c => c.CarCategories)
                .HasForeignKey(cc => cc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}