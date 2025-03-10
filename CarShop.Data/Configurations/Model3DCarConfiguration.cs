using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class Model3DCarConfiguration : IEntityTypeConfiguration<Model3DCar>
    {
        public void Configure(EntityTypeBuilder<Model3DCar> builder)
        {
            builder.HasKey(mc => new { mc.Model3dId, mc.CarId });

            builder.HasOne(mc => mc.Car)
               .WithMany(mc => mc.Model3DCars)
               .HasForeignKey(cc => cc.CarId)
               .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(amc => amc.Model3D)
                .WithMany(amc => amc.Model3DCars)
                .HasForeignKey(amc => amc.Model3dId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}