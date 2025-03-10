using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class Category: IEntity
    {
        public int CategoryId { get; set; }        // Kategorinin benzersiz ID'si
        public string Name { get; set; }    // Kategori adı (örneğin: "SUV", "Jeep", "Ağır Vasıta")

        public string? ImageUrl { get; set; } // kategori resimleri

        // Araçlar ile ilişki
        public List<CarCategory> CarCategories { get; set; }
        public List<HighlightedCarCategory> highlightedCarCategories {get; set;}

    }

}