using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
         

        // Kategori için birincil anahtar
        builder.HasKey(c => c.CategoryId);

        // Kategorinin isminin benzersiz olmasını sağlamak için
        builder.HasIndex(c => c.Name).IsUnique();

        // Araçlar ile ilişki (Bire çok ilişki)
        builder.HasMany(c => c.CarCategories)  // Bir Category birden fazla CarCategory'ye sahip olabilir
            .WithOne(cc => cc.Category)  // Her CarCategory bir Category'ye sahiptir
            .HasForeignKey(cc => cc.CategoryId)  // Yabancı anahtar (CategoryId)
            .OnDelete(DeleteBehavior.Cascade);  // Silme işlemi ile ilişkili CarCategory'lerin de silinmesini sağlar
           
    
        }
    }
}