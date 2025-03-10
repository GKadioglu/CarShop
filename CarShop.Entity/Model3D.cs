using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class Model3D: IEntity
    {
       public int Model3dId { get; set; }
       public string ModelUrl { get; set; }
       public List<Model3DCar> Model3DCars {get; set;}
    }
}