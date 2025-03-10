using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.obj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class AnonimMessageCarConfiguration: IEntityTypeConfiguration<AnonimMessageCar>
    {
        public void Configure(EntityTypeBuilder<AnonimMessageCar> builder)
        {
             builder.HasKey(amc => new { amc.CarId, amc.MessageId });

            builder.HasOne(amc => amc.Car)
                .WithMany(amc => amc.AnonimMessageCars)
                .HasForeignKey(cc => cc.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(amc => amc.AnonimMessage)
                .WithMany(amc => amc.AnonimMessageCars)
                .HasForeignKey(amc => amc.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}