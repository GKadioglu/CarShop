using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class CarCarGalleryConfiguration : IEntityTypeConfiguration<CarCarGallery>
    {
        public void Configure(EntityTypeBuilder<CarCarGallery> builder)
        {
            builder.HasKey(cm => new { cm.CarId, cm.CarGalleryId });

            // Car ile ilişki
            builder.HasOne(cm => cm.Car)
                .WithMany(c => c.CarCarGalleries)
                .HasForeignKey(cm => cm.CarId)
                .OnDelete(DeleteBehavior.Cascade);

                // CarGallery ile ilişki
            builder.HasOne(cm => cm.CarGallery)
                .WithMany(m => m.CarCarGalleries)
                .HasForeignKey(cm => cm.CarGalleryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}