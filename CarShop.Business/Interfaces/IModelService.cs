using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;

namespace CarShop.Business.Abstract
{
    public interface IModelService
    {
        Model GetModelByName(string name);

        List<Model> GetModels(int? Id);

        List<Model> GetModelsByYearRange(int startYear, int endYear);

        void Create(Model model);
        Task<Model> CreateAsync(Model model);
        void Update(Model model);
        void Delete(Model model);
        void DeleteFromModel(int CarId, int ModelId);
    }
}