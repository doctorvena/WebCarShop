using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_CS.EF_Models
{
    public class CarModel
    {
        public int ID { get; set; }
        public string NazivModela { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
    }
}
