using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {

            // ID için yapılandırma
            builder.HasKey(m => m.ModelId);

            // Model adı için yapılandırma
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Origin)
                .HasMaxLength(100);

            builder.Property(m => m.EstablishmentYear)
                .IsRequired();

            builder.Property(m => m.Founder)
                .HasMaxLength(100);

           
        }
    }
}