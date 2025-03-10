using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity.Models
{
    public class Cars3DModel
    {
        public int Model3dId { get; set; }
        public string ModelUrl { get; set; }
        public List<Cars3DModelCarId> Cars { get; set; }
    }

    public class Cars3DModelCarId 
    {
        public int CarId { get; set; }
    }

}