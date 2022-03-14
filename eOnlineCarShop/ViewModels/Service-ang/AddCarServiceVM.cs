using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels.Service_ang
{
    public class AddCarServiceVM
    {
        public int ID { get; set; } 
        public string Warnings { get; set; }
        public string Recommendations { get; set; }
        public string ServiceName { get; set; }
        public DateTime DateofServiceWarranty { get; set; }
        public DateTime DateOfServiced { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string SelectedCar { get; set; }
        public List<SelectListItem> Cars { get; set; }
    }
}
