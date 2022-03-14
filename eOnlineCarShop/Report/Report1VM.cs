using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportDesignApp.Report
{
    public class Report1VM
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string CarModel { get; set; }
        public string Fuel { get; set; }
        public string VehicleType { get; set; }
        public string Color { get; set; }
        public string DriveType { get; set; }
        public string Transmission { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfDors { get; set; }
        public string NumberOfGears { get; set; }
        public int PowerPS { get; set; }
        public int PowerKw { get; set; }
        public float WheelSize { get; set; }
        public float Ccm { get; set; }
        public float Kilometre { get; set; }
        public string DateOfManufacture { get; set; }
        public string Price { get; set; }
        public static List<Report1VM> Get()
        {
            return new List<Report1VM> { };
        }
    }
}