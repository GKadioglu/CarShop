using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class HighlightedCarConfiguration : IEntityTypeConfiguration<HighlightedCar>
    {
        public void Configure(EntityTypeBuilder<HighlightedCar> builder)
        {
            builder.HasKey(c => c.HighlightedCarId);
            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Year)
                .IsRequired();
            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");  // Para birimi için
        }
    }
}