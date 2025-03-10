using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Data.Abstract;
using CarShop.Entity;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public List<Category> GetAllCategory()
        {
            return ShopContext.Categories.ToList();
        }

        public List<Category> GetCategoriesByCarId(int carId)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategoriesOrderedByName(bool ascending)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategoriesWithCarCount()
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            return ShopContext.Categories.
                                        Where(i => i.CategoryId == id)
                                        .Include(i => i.CarCategories)
                                        .ThenInclude(i => i.Car)
                                        .FirstOrDefault();
        }

        public Category GetCategoryByName(string categoryName)
        {
            return ShopContext.Categories
                .Where(i => EF.Functions.Like(i.Name, categoryName)) 
                .Include(i => i.CarCategories)
                    .ThenInclude(i => i.Car)
                .FirstOrDefault();
        }

        public List<Category> SearchCategoriesByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}