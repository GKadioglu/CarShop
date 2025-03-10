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
    public class EfCoreModel3DRepository : EfCoreGenericRepository<Model3D>, IModel3DRepository
    {

        public EfCoreModel3DRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public Model3D GetModel3D(int carId)
        {
                return ShopContext.Model3D
                .Include(m => m.Model3DCars)
                .FirstOrDefault(m => m.Model3DCars
                    .Any(mc => mc.CarId == carId));
        }
    }
}