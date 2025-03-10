using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Data.Abstract;
using CarShop.Entity;

namespace CarShop.Business.Concrete
{
    public class ModelManager: IModelService
    {
        private readonly IUnitOfWork _unitofwork;

        public ModelManager(IUnitOfWork unitofwork)
        {
            _unitofwork =  unitofwork;
        }

        public void Create(Model model)
        {
            throw new NotImplementedException();
        }

        public Task<Model> CreateAsync(Model model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Model model)
        {
            throw new NotImplementedException();
        }

        public void DeleteFromModel(int CarId, int ModelId)
        {
            throw new NotImplementedException();
        }

        public Model GetModelByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Model> GetModels(int? Id)
        {
            return _unitofwork.Models.GetModels(Id);
        }

        public List<Model> GetModelsByOrigin(string origin)
        {
            throw new NotImplementedException();
        }

        public List<Model> GetModelsByYearRange(int startYear, int endYear)
        {
            throw new NotImplementedException();
        }

        public void Update(Model model)
        {
            throw new NotImplementedException();
        }
    }
}