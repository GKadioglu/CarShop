using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class HighlightedCarModelConfiguration : IEntityTypeConfiguration<HighlightedCarModel>
    {
        public void Configure(EntityTypeBuilder<HighlightedCarModel> builder)
        {
             // Birincil anahtar
            builder.HasKey(cm => new { cm.HighlightedCarId, cm.ModelId });

            // Car ile ilişki
            builder.HasOne(cm => cm.HighlightedCar)
                .WithMany(c => c.HighlightedCarModels)
                .HasForeignKey(cm => cm.HighlightedCarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Model ile ilişki
            builder.HasOne(cm => cm.Model)
                .WithMany(m => m.highlightedCarModels)
                .HasForeignKey(cm => cm.ModelId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}