using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class Model3DCar: IEntity
    {
        public int CarId { get; set; }   
        public Car Car { get; set; }

        public int Model3dId { get; set; }
        public Model3D Model3D {get; set;}
    }
}