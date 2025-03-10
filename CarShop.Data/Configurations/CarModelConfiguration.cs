using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
    {
        public void Configure(EntityTypeBuilder<CarModel> builder)
        {

            // Birincil anahtar
            builder.HasKey(cm => new { cm.CarId, cm.ModelId });

            // Car ile ilişki
            builder.HasOne(cm => cm.Car)
                .WithMany(c => c.CarModels)
                .HasForeignKey(cm => cm.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Model ile ilişki
            builder.HasOne(cm => cm.Model)
                .WithMany(m => m.CarModels)
                .HasForeignKey(cm => cm.ModelId)
                .OnDelete(DeleteBehavior.Cascade);

          
            
        }
        
    }
}