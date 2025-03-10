using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
        public class AnonimMessageConfiguration : IEntityTypeConfiguration<AnonimMessage>
        {
            public void Configure(EntityTypeBuilder<AnonimMessage> builder)
            {
                builder.HasKey(m => m.MessageId); 
                builder.Property(m => m.MessageId)
                    .ValueGeneratedOnAdd(); 
                builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(100);
                builder.Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(100);

                builder.Property(m => m.Message)
                    .IsRequired()
                    .HasMaxLength(1000);


            }
    }
}