using System;
using System.Collections.Generic;
using System.Text;

namespace Data_CS.EF_Models
{
    public class ServicedCars
    {
        public int ID { get; set; }
        public string ServiceName { get; set; }
        public DateTime DateOfServiced { get; set; }
       public DateTime DateofServiceWarranty { get; set; }
        public string Description { get; set; }
        public string Recommendations { get; set; }
        public string Warnings { get; set; }
        public float Price { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }

    }
}
