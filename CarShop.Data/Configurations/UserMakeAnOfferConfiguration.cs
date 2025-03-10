using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class UserMakeAnOfferConfiguration: IEntityTypeConfiguration<UserMakeAnOffer>
    {
        public void Configure(EntityTypeBuilder<UserMakeAnOffer> builder)
            {
                builder.HasKey(u => u.UserOfferId); 
                builder.Property(u => u.UserOfferId)
                    .ValueGeneratedOnAdd(); 
                builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);
                builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

                builder.Property(u => u.Offer)
                    .IsRequired();
            }
    }
}