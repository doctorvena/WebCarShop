using AspNetCore.Reporting;
using Data_CS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ReportDesignApp;
using ReportDesignApp.Report;

namespace eOnlineCarShop.Controllers
{
    public class Report1Controller : Controller
    {
        private ApplicationDbContext _db;

        public Report1Controller(ApplicationDbContext db)
        {
            _db = db;
        }

        public static List<Report1VM> getCar(ApplicationDbContext db, int CarId)
        {
            List<Report1VM> podaci = db.Car
                .Where(w => w.ID == CarId)
                .Select(s => new Report1VM
                {
                    Brand = s.brand.BrandName,
                    CarModel = s.Model,
                    NumberOfSeats = s.NumberOfSeats,
                    NumberOfDors = s.NumberOfDors,
                    NumberOfGears = s.NumberOfGears,
                    PowerKw = s.PowerKw,
                    PowerPS = s.PowerPS,
                    Ccm = s.Ccm,
                    WheelSize = s.WheelSize,
                    Kilometre = s.Kilometre,
                    DateOfManufacture = s.DateOfManufacture.ToString("yyyy"),
                    Fuel = s.Fuel.FuelName,
                    VehicleType = s.VehicleType.TypeName,
                    Color = s.Color.ColorName,
                    DriveType = s.DriveType.DriveTypeName,
                    Transmission = s.Transmission.TransmissionType,
                    Price = s.Price.ToString() + " KM"
                }).ToList();

            return podaci;
        }
        public IActionResult Index(int CarId)
        {
            AspNetCore.Reporting.LocalReport _localReport = new AspNetCore.Reporting.LocalReport("Report/Report1.rdlc");
            List<Report1VM> podaci = getCar(_db, CarId);
            _localReport.AddDataSource("DS_Car", podaci);

            string cijena = _db.Car.Where(w => w.ID == CarId).Select(s => s.Price.ToString()).FirstOrDefault() + " KM";
            if (cijena == null)
                cijena = "Not set";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Cijena", cijena);

            ReportResult result = _localReport.Execute(RenderType.Pdf, parameters: parameters);
            return File(result.MainStream, "application/pdf");
        }
    }
}
