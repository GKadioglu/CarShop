using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            // Primary key
            builder.HasKey(c => c.CarId);

            // CarId Identity (otomatik artan olarak ayarlandı)
            builder.Property(c => c.CarId)
                .ValueGeneratedOnAdd();

            // Brand alanı zorunlu ve maksimum uzunluğu 100 karakter
            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100);

            // Model alanı zorunlu ve maksimum uzunluğu 100 karakter
            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(100);

            // Year alanı zorunlu
            builder.Property(c => c.Year)
                .IsRequired();

            // Price alanı zorunlu ve para birimi için uygun format
            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // İlişkiler ve diğer yapılandırmalar burada eklenebilir
        }
    }
}
