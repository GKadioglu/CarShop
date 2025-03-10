using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class Model3DConfiguration : IEntityTypeConfiguration<Model3D>
    {
        public void Configure(EntityTypeBuilder<Model3D> builder)
        {
            builder.HasKey(m => m.Model3dId);
            builder.Property(m => m.Model3dId)
                      .ValueGeneratedOnAdd();
            builder.Property(m => m.ModelUrl)
            .IsRequired()
            .HasMaxLength(100);
        }
    }
}