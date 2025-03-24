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

        Category GetCategoryById(int id);   

        List<Category> GetAllCategory();

        List<Category> GetCategoriesWithCarCount();

        List<Category> GetCategoriesByCarId(int carId);

        List<Category> SearchCategoriesByName(string name);

        List<Category> GetCategoriesOrderedByName(bool ascending);
    }
}