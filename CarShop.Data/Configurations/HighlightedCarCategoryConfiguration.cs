using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class HighlightedCarCategoryConfiguration : IEntityTypeConfiguration<HighlightedCarCategory>
    {
        public void Configure(EntityTypeBuilder<HighlightedCarCategory> builder)
        {
            builder.HasKey(cc => new { cc.HighlightedCarId, cc.CategoryId });

            // Car ile ilişki
            builder.HasOne(cc => cc.HighlightedCar)
                .WithMany(c => c.HighlightedCarCategories)
                .HasForeignKey(cc => cc.HighlightedCarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category ile ilişki
            builder.HasOne(cc => cc.Category)
                .WithMany(c => c.highlightedCarCategories)
                .HasForeignKey(cc => cc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}