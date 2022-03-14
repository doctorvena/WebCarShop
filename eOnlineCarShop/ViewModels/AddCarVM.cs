using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class AddCarVM
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public List<SelectListItem> Brendovi { get; set; }
        public int CarModelID { get; set; }
        public List<SelectListItem> Modeli { get; set; }
        public int FuelID { get; set; }
        public List<SelectListItem> FuelName { get; set; }
        public int VehicleTypeID { get; set; }
        public List<SelectListItem> TypeName { get; set; }
        public int ColorID { get; set; }
        public List<SelectListItem> Color { get; set; }
        public int DriveTypeID { get; set; }
        public List<SelectListItem> DriveType { get; set; }
        public int TransmissionID { get; set; }
        public List<SelectListItem> Transmission { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfDors { get; set; }
        public string NumberOfGears { get; set; }
        public int PowerPS { get; set; }
        public int PowerKw { get; set; }
        public float WheelSize { get; set; }
        public float Ccm { get; set; }
        public float Kilometre { get; set; }
        public float Price { get; set; }
        public DateTime DateOfManufacture { get; set; }

        [BindProperty]
        public List<IFormFile> Images { get; set; }
    }
}
