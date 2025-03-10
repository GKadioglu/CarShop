using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class AdminMessageAnonimMessageConfiguration: IEntityTypeConfiguration<AdminMessageAnonimMessage>
    {
        public void Configure(EntityTypeBuilder<AdminMessageAnonimMessage> builder)
        {
            builder.HasKey(amc => new { amc.MessageId, amc.AdminMessageId });

            builder.HasOne(amc => amc.AnonimMessage)
                .WithMany(amc => amc.AdminMessageAnonimMessages)
                .HasForeignKey(cc => cc.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(amc => amc.AdminMessage)
                .WithMany(amc => amc.AdminMessageAnonimMessages)
                .HasForeignKey(amc => amc.AdminMessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}