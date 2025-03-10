using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {

        Category GetCategoryByName(string categoryName);

        // Id'ye göre kategorileme işlemi
        Category GetCategoryById(int id);   

        // tüm kategorileri getir.
        List<Category> GetAllCategory();

        // Tüm kategorileri içerdikleri araç sayısıyla birlikte getirme
        List<Category> GetCategoriesWithCarCount();

        // Belirli bir araç ID'sine sahip kategorileri getirme
        List<Category> GetCategoriesByCarId(int carId);

        // Kategori adı benzerlik araması (örneğin, "SUV" ile başlayanlar)
        List<Category> SearchCategoriesByName(string name);

        // Kategorileri alfabetik sıraya göre sıralama
        List<Category> GetCategoriesOrderedByName(bool ascending);
    }
}