using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels.Service_ang
{
    public class EditCarServiceVM
    {
        public int id { get; set; }
        public string warnings { get; set; }
        public string recommendations { get; set; }
        public string serviceName { get; set; }
        public DateTime dateofServiceWarranty { get; set; }
        public DateTime dateOfServiced { get; set; }
        public float price { get; set; }
        public string selectedCar { get; set; }
    }
}
