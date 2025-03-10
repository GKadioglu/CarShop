using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class AdminMakeAnOfferUserMakeAnOfferConfiguration: IEntityTypeConfiguration<AdminMakeAnOfferUserMakeAnOffer>
    {
        public void Configure(EntityTypeBuilder<AdminMakeAnOfferUserMakeAnOffer> builder)
        {
            builder.HasKey(ama => new { ama.AdminOfferId, ama.UserOfferId });

            builder.HasOne(ama => ama.AdminMakeAnOffer)
                .WithMany(ama => ama.AdminMakeAnOfferUserMakeAnOffers)
                .HasForeignKey(cc => cc.AdminOfferId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(ama => ama.UserMakeAnOffer)
                .WithMany(ama => ama.AdminMakeAnOfferUserMakeAnOffers)
                .HasForeignKey(ama => ama.UserOfferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}