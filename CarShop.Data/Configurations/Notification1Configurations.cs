using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class Notification1Configurations : IEntityTypeConfiguration<Notifications1>
    {
        public void Configure(EntityTypeBuilder<Notifications1> builder)
        {
            // ID için yapılandırma
            builder.HasKey(n => n.NotificationsId);

            builder.Property(m => m.Sender)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.CreatedDate)
            .IsRequired()
                .HasMaxLength(100);

        }


    }
}