using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class AdminMakeAnOfferConfiguration : IEntityTypeConfiguration<AdminMakeAnOffer>
    {
        public void Configure(EntityTypeBuilder<AdminMakeAnOffer> builder)
        {

            builder.HasKey(a => a.AdminOfferId);
            builder.Property(a => a.AdminOfferId)
                      .ValueGeneratedOnAdd();
            builder.Property(a => a.Sender)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.Receiver)
        .IsRequired()
        .HasMaxLength(100);
            builder.Property(a => a.Acceptance)
                       .IsRequired();

        }
    }
}