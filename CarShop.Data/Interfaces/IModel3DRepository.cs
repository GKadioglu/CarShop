using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface IModel3DRepository: IRepository<Model3D>
    {
        Model3D GetModel3D (int carId);
    }
}