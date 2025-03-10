using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Data.Abstract;
using CarShop.Entity;
using Core.DataAccess.EntityFramework;

namespace CarShop.Data.Concrete.EfCore
{
    public class EfCoreModelRepository : EfCoreGenericRepository<Model>, IModelRepository
    {
        public EfCoreModelRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public Model GetModelByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Model> GetModels(int? Id)
        {
            if (Id.HasValue)
            {
                return ShopContext.Models.Where(m => m.ModelId == Id.Value).ToList();
            }
            else
            {
                return ShopContext.Models.ToList(); // Id sağlanmazsa tüm modelleri döner.
            }
        }

        public List<Model> GetModelsByOrigin(string origin)
        {
            throw new NotImplementedException();
        }

        public List<Model> GetModelsByYearRange(int startYear, int endYear)
        {
            throw new NotImplementedException();
        }
    }
}