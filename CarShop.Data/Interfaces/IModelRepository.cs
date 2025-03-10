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
    // Model adına göre model getirme
    Model GetModelByName(string name);

    // Üretim menşeine göre model getirme
    List<Model> GetModels(int? Id);

    // Yıl aralığına göre model getirme
    List<Model> GetModelsByYearRange(int startYear, int endYear);

    }
}