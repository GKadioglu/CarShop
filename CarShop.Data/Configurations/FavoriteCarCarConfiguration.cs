using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class FavoriteCarCarConfiguration : IEntityTypeConfiguration<FavoriteCarCar>
    {
        public void Configure(EntityTypeBuilder<FavoriteCarCar> builder)
        {
            builder.HasKey(fv => new {fv.FavoriteId, fv.CarId});

            // Car ile ilişki
            builder.HasOne(fv => fv.Car)
            .WithMany(c=>c.FavoriteCarCars)
            .HasForeignKey(fv=>fv.CarId)
            .OnDelete(DeleteBehavior.Cascade);

            // favoriteCar ile ilişki

            builder.HasOne(fv=>fv.FavoriteCar)
            .WithMany(f=>f.FavoriteCarCars)
            .HasForeignKey(fv=>fv.FavoriteId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}