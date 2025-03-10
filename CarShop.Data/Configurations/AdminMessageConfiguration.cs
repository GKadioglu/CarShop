using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class AdminMessageConfiguration : IEntityTypeConfiguration<AdminMessage>
    {
        public void Configure(EntityTypeBuilder<AdminMessage> builder)
        {

            builder.HasKey(m => m.AdminMessageId);
            builder.Property(m => m.AdminMessageId)
                      .ValueGeneratedOnAdd();
            builder.Property(m => m.Sender)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Receiver)
        .IsRequired()
        .HasMaxLength(100);
         builder.Property(m => m.Message)
                    .IsRequired()
                    .HasMaxLength(1000);

        }
    }
}