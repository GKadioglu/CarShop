using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class NotificationUser1Configurations : IEntityTypeConfiguration<NotificationUser1>
    {
        public void Configure(EntityTypeBuilder<NotificationUser1> builder)
        {
            // ID için yapılandırma
            builder.HasKey(nu => nu.Id);

            // Foreign Key: NotificationsId -> Notifications
            builder.HasOne(nu => nu.Notification) // Bir NotificationsUser kaydı, bir Notification ile ilişkilidir.
                .WithMany(n => n.NotificationsUsers) // Bir Notification, birden fazla NotificationsUser içerebilir.
                .HasForeignKey(nu => nu.NotificationsId) // FK olarak NotificationsId'yi kullan
                .OnDelete(DeleteBehavior.Cascade); // Notification silinirse, ona bağlı NotificationUser1 kayıtları da silinir

            // UserName zorunlu alan
            builder.Property(nu => nu.UserName)
                .HasMaxLength(100)
                .IsRequired();

            // Read alanı zorunlu alan
            builder.Property(nu => nu.Reads)
                .IsRequired();
        }
    }
}