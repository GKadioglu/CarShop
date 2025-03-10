using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Data.Abstract;
using CarShop.Entity.Models;

namespace CarShop.Business.Concrete
{
    public class Model3DManager : IModel3DService
    {
        private readonly IUnitOfWork _unitofwork;

        public Model3DManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public Cars3DModel GetModel3D(int carId)
        {
            var model3D = _unitofwork.Model3D.GetModel3D(carId);
            if (model3D == null)
            {
                return null; // Ya da özel bir hata durumu döndürebilirsin
            }
            // Model3D'yi Cars3DModel formatına dönüştür
            var cars3DModel = new Cars3DModel
            {
                Model3dId = model3D.Model3dId,
                ModelUrl = model3D.ModelUrl,
                Cars = model3D.Model3DCars
                    .Select(mc => new Cars3DModelCarId
                    {
                        CarId = mc.CarId
                    }).ToList()
            };

            return cars3DModel;
        }
    }
}