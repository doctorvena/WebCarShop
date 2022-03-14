using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsers() // metoda koja vraca podatke za Angular
        {
            var model = _db.User
                .Select(s => new ShowUsersVM {
                userid = s.Id,
                firstname = s.FirstName,
                lastname = s.LastName,
                birthdate = s.BirthDate.ToString(),
                username = s.UserName,
                email = s.Email,
                emailconfirmed = s.EmailConfirmed,
                phonenumber = s.PhoneNumber,
                phonenumberconfirmed = s.PhoneNumberConfirmed
            }).ToList();

            return Ok(model);
        }

        public IActionResult ShowDeletedCars(int carID)
        {
            List<ShowCarsVM> model = _db.Car
                .Where(w=> w.IsDeleted == true)
                .Select(s => new ShowCarsVM
            {
                CarId = s.ID,
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
                DateOfManufacture = s.DateOfManufacture,
                Fuel = s.Fuel.FuelName,
                VehicleType = s.VehicleType.TypeName,
                Color = s.Color.ColorName,
                DriveType = s.DriveType.DriveTypeName,
                Transmission = s.Transmission.TransmissionType
            }).ToList();

            foreach (var item in model)
            {
                var carImageSET = _db.CarImage.Where(x => x.CarID == item.CarId).ToList();
                var AllImages = new List<string>();

                foreach (var slika in carImageSET)
                {
                    var ImageEntity = _db.Image.Where(i => i.ID == slika.ImageID).Select(i => i.PathToImage).FirstOrDefault();
                    AllImages.Add(ImageEntity);
                }
                item.images = AllImages;
            }

            return View(model);
        }

    }
}
