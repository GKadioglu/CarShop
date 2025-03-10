using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarShop.Entity;
using CarShop.Data.Configurations;
using CarShop.Entity.obj;

namespace CarShop.Data.Concrete
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        // DbSet'ler (Tablolar)
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<HighlightedCar> HighlightedCars { get; set; }
        public DbSet<HighlightedCarModel> HighlightedCarModels { get; set; }
        public DbSet<HighlightedCarCategory> HighlightedCarCategories { get; set; }

        public DbSet<CarGallery> CarGalleries { get; set; }
        public DbSet<CarCarGallery> CarCarGalleries { get; set; }

        public DbSet<FavoriteCar> FavoriteCars { get; set; }
        public DbSet<FavoriteCarCar> FavoriteCarCars { get; set; }

        public DbSet<AnonimMessage> AnonimMessages { get; set; }
        public DbSet<AnonimMessageCar> AnonimMessageCars { get; set; }
        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<AdminMessageAnonimMessage> AdminMessageAnonimMessages { get; set; }

        public DbSet<UserMakeAnOffer> UserMakeAnOffers { get; set; }
        public DbSet<MakeAnOfferCar> MakeAnOfferCars { get; set; }
        public DbSet<AdminMakeAnOffer> AdminMakeAnOffers { get; set; }
        public DbSet<AdminMakeAnOfferUserMakeAnOffer> AdminMakeAnOfferUserMakeAnOffers { get; set; }

        public DbSet<Model3D> Model3D { get; set; }
        public DbSet<Model3DCar> Model3DCars { get; set; }

        public DbSet<Notifications1> Notifications1 { get; set; }
        public DbSet<NotificationUser1> NotificationsUsers1 { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new CarModelConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());

            modelBuilder.ApplyConfiguration(new HighlightedCarConfiguration());
            modelBuilder.ApplyConfiguration(new HighlightedCarCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new HighlightedCarModelConfiguration());

            modelBuilder.ApplyConfiguration(new CarGalleryConfiguration());
            modelBuilder.ApplyConfiguration(new CarCarGalleryConfiguration());

            modelBuilder.ApplyConfiguration(new FavoriteCarConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteCarCarConfiguration());

            modelBuilder.ApplyConfiguration(new AnonimMessageConfiguration());
            modelBuilder.ApplyConfiguration(new AnonimMessageCarConfiguration());

            modelBuilder.ApplyConfiguration(new AdminMessageConfiguration());
            modelBuilder.ApplyConfiguration(new AdminMessageAnonimMessageConfiguration());

            modelBuilder.ApplyConfiguration(new UserMakeAnOfferConfiguration());
            modelBuilder.ApplyConfiguration(new MakeAnOfferCarConfiguration());

            modelBuilder.ApplyConfiguration(new AdminMakeAnOfferConfiguration());
            modelBuilder.ApplyConfiguration(new AdminMakeAnOfferUserMakeAnOfferConfiguration());

            modelBuilder.ApplyConfiguration(new Model3DConfiguration());
            modelBuilder.ApplyConfiguration(new Model3DCarConfiguration());

            modelBuilder.ApplyConfiguration(new Notification1Configurations());
            modelBuilder.ApplyConfiguration(new NotificationUser1Configurations());

        }
    }

}