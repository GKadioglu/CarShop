using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class MakeAnOfferCarConfiguration: IEntityTypeConfiguration<MakeAnOfferCar>
    {
         public void Configure(EntityTypeBuilder<MakeAnOfferCar> builder)
        {
             builder.HasKey(mao => new { mao.CarId, mao.OfferId });

            builder.HasOne(mao => mao.Car)
                .WithMany(mao => mao.MakeAnOfferCars)
                .HasForeignKey(cc => cc.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(mao => mao.MakeAnOffer)
                .WithMany(mao => mao.MakeAnOfferCars)
                .HasForeignKey(mao => mao.OfferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}