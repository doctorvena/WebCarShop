using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels.Service_ang
{
    public class DetailsCarServiceVM
    {
        public int ID { get; set; }
        public string ServiceName { get; set; }
        public string DateOfServiced { get; set; }
        public string DateofServiceWarranty { get; set; }
        public string Recommendations { get; set; }
        public string Warnings { get; set; }
        public float Price { get; set; }
        public string SelectedCar { get; set; }
    }
}
