using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.Models;

namespace CarShop.Business.Abstract
{
    public interface IModel3DService
    {
        Cars3DModel GetModel3D (int carId);
    }
}