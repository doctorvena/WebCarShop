using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels.UserWishList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    [Produces("application/json")]
    public class AngularUserController : Controller
    {

        private ApplicationDbContext _db;
        public AngularUserController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Prikaz(string q, int currentPage = 1, int itemsPerPage = 5)
        {

            List<ListUserWishlistVM.Row> studenti = _db.Car
                .Where(s => q == null || (s.Model + " " + s.Model).StartsWith(q) ||
                            (s.Model + " " + s.Model).StartsWith(q))
                .Select(x => new ListUserWishlistVM.Row
                {
                    id = x.ID,
                    brand = _db.Brand.Where(i => i.ID == x.BrandID).Select(i => i.BrandName).FirstOrDefault(),
                    model = x.Model,
                    fuel = _db.Fuel.Where(i => i.ID == x.FuelID).Select(i => i.FuelName).FirstOrDefault(),
                    color = _db.Color.Where(i => i.ID == x.ColorID).Select(i => i.ColorName).FirstOrDefault(),
                    numberOfGears = x.NumberOfGears,
                    powerKw = x.PowerKw,
                    wheelSize = x.WheelSize,
                })
                .ToList();

            ListUserWishlistVM model = new ListUserWishlistVM();
            model.list = studenti;
            model.total = studenti.Count();
            model.q = q;

            return Ok(model);
        }
        [HttpGet]
        public IActionResult Index(string q)
        {
            //select * from Student 
            List<ListUserWishlistVM.Row> wisHLIST = _db.UserWishlist
                .Where(s => q == null || (s.model + " " + s.model).StartsWith(q) ||
                            (s.model + " " + s.model).StartsWith(q))
                .Select(x => new ListUserWishlistVM.Row
                {
                    id = x.id,
                    brand = x.brand,
                    model = x.model,
                    fuel = x.fuel,
                    color = x.color,
                    numberOfGears = x.numberOfGears,
                    powerKw = x.powerKw,
                    wheelSize = x.wheelSize,
                })
                .ToList();

            ListUserWishlistVM m = new ListUserWishlistVM();
            m.list = wisHLIST;
            m.total = wisHLIST.Count();
            m.q = q;

            return Ok(m);
        }

        [HttpPost]
        public IActionResult Snimi([FromBody] ListUserWishlistVM.Row x)
        {
            UserWishlist model = _db.UserWishlist.Single(s => s.id == x.id);
            model.brand = x.brand;
            model.model = x.model;
            model.fuel = x.fuel;
            model.color = x.color;
            model.numberOfGears = x.numberOfGears;
            model.powerKw = 15;
            model.wheelSize = 15;

            _db.SaveChanges();

            return Ok();
        }
        public IActionResult Dodaj([FromBody] ListUserWishlistVM.Row x)
        {
            if (x != null)
            {
                UserWishlist newdata = new UserWishlist()
                {
                    brand = x.brand,
                    model = x.model,
                    fuel = x.fuel,
                    color = x.color,
                    numberOfGears = x.numberOfGears,
                    powerKw = 15,
                    wheelSize = 15
                };
                _db.UserWishlist.Add(newdata);
                _db.SaveChanges();
            }

            return Ok();
        }
        //public IActionResult Edit(int ID)
        //{
        //    ServicedCars servis = _db.ServicedCars.Where(x => x.ID == ID).Include(x => x.Car).FirstOrDefault();

        //    EditUserWishlistVM model = new EditUserWishlistVM
        //    {
        //        //id = servis.ID,
        //        //serviceName = servis.ServiceName,
        //        //dateOfServiced = servis.DateOfServiced,
        //        //selectedCar = servis.Car.Model,// onemogucit uredjivanje 
        //        //price = servis.Price,
        //        //recommendations = servis.Recommendations,
        //        //warnings = servis.Warnings,
        //        //dateofServiceWarranty = servis.DateofServiceWarranty
        //    };
        //    return Ok(model);
        //}

        public IActionResult Obrisi(int itemId)
        {
            UserWishlist s = _db.UserWishlist.Single(s => s.id == itemId);

            _db.Remove(s);
            _db.SaveChanges();//delete where Id=...

            TempData["PorukaWarning"] = "Uspješno ste obrisali stavku"; //transport podataka iz akcije 1 u (akciju 2 + njegov view)


            return Ok();
        }
    }
}
