using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CarShop.Data.Configurations
{
    public class CarGalleryConfiguration : IEntityTypeConfiguration<CarGallery>
    {
        public void Configure(EntityTypeBuilder<CarGallery> builder)
        {
            builder.HasKey(c => c.CarGalleryId);
        }
    }
}