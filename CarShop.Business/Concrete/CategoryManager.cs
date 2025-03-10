using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Business.Contants;
using CarShop.Data.Abstract;
using CarShop.Entity;
using Core.Utilities.Results;
using static CarShop.Entity.Models.CategoryModel;

namespace CarShop.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IResult Create(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> CreateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Category category)
        {
            throw new NotImplementedException();
        }


        public IDataResult<List<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Category>> GetAllCategory()
        {
            var allCategory = _unitofwork.Categories.GetAllCategory();

            if (allCategory != null && allCategory.Any())
            {
                
                return new SuccessDataResult<List<Category>>(allCategory, CategoryMessages.CategoryFound);
            }

            
            return new ErrorDataResult<List<Category>>(CategoryMessages.CategoryNotFound);
        }

        public IDataResult<Task<Category>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Category> GetByIdWithCars(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Category> GetCategoryById(int id)
        {
            var categories = _unitofwork.Categories.GetCategoryById(id);

            if (categories != null)
            {
                return new SuccessDataResult<Category>(categories, CategoryMessages.CategoryFound);
            }
            return new ErrorDataResult<Category>(CategoryMessages.CategoryNotFound);
        }

        public IDataResult<GetCategoryByNameModel> GetCategoryByName(string categoryName)
        {
            var categoryByName = _unitofwork.Categories.GetCategoryByName(categoryName);

            if (categoryByName == null)
            {
                return new ErrorDataResult<GetCategoryByNameModel>(CategoryMessages.CategoryNotFound);
            }

            var categoryModel = new GetCategoryByNameModel
            {
                CategoryId = categoryByName.CategoryId,
                Name = categoryByName.Name,
                Cars = categoryByName.CarCategories.Select(cc => new CarCategoryModel
                {
                    CarId = cc.CarId,
                    Brand = cc.Car.Brand,
                    Model = cc.Car.Model,
                    Year = cc.Car.Year,
                    Price = cc.Car.Price,
                    ImageUrl = cc.Car.ImageUrl
                }).ToList()
            };
            return new SuccessDataResult<GetCategoryByNameModel>(categoryModel, CategoryMessages.CategoryFound);
        }

        public IResult Update(Category category)
        {
            throw new NotImplementedException();
        }

        IDataResult<List<Category>> ICategoryService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}