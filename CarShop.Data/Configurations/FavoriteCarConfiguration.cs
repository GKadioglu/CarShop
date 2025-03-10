using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class FavoriteCarConfiguration : IEntityTypeConfiguration<FavoriteCar>
    {
        public void Configure(EntityTypeBuilder<FavoriteCar> builder)
        {
            // Primary Key
           builder.HasKey(f=>f.FavoriteId);

           
        }
    }
}