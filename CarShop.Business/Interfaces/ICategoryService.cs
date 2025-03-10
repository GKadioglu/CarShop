using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.Utilities.Results;
using static CarShop.Entity.Models.CategoryModel;

namespace CarShop.Business.Abstract
{
    public interface ICategoryService
    {

        IDataResult<Category> GetCategoryById(int id);
        IDataResult<Task<Category>> GetById(int id);
        IDataResult<Category> GetByIdWithCars(int categoryId);
        IDataResult<List<Category>> GetAll();
        IDataResult<List<Category>> GetAllCategory();
        IResult Create(Category category);
        Task<IResult> CreateAsync(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
        IDataResult<GetCategoryByNameModel> GetCategoryByName(string categoryName);
    }
}