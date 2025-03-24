using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface IModelRepository: IRepository<Model>
    {
    Model GetModelByName(string name);

    List<Model> GetModels(int? Id);

    List<Model> GetModelsByYearRange(int startYear, int endYear);

    }
}