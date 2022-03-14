using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace eOnlineCarShop.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _db;
        private readonly IToastNotification _nToastNotify;

        public ShopController(ApplicationDbContext db, IToastNotification nToastNotify)
        {
            _db = db;
            _nToastNotify = nToastNotify;
        }
        public IActionResult ShopViewDiv(string txtunos)
        {
            List<ShowCarsVM> model = _db.Car.Select(s => new ShowCarsVM
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

            var shoplista = model.Where(s => txtunos == "" || txtunos == null || (s.Brand.ToLower().Contains(txtunos.ToLower())
                           || s.CarModel.ToLower().Contains(txtunos.ToLower()))).ToList();

            ViewData["txtUnosKey"] = txtunos;
            ViewData["ShopListKey"] = shoplista;


            return View();
        }
        public IActionResult CarDetails(int id)
        {

            var car = _db.Car
                .Where(i => i.ID == id)
                .SingleOrDefault();

            //var car = _db.Car.Find(model.CarId);

            var modelDetails = new ShowCarDetailsVM
            {
                carID = car.ID,
                Brand = _db.Brand.Where(s => s.ID == car.BrandID).Select(s => s.BrandName).FirstOrDefault(),
                Model = car.Model,
                FuelID = car.FuelID,
                Fuel = _db.Fuel.Where(s => s.ID == car.FuelID).Select(s => s.FuelName).FirstOrDefault(),
                VehicleTypeID = car.VehicleTypeID,
                VehicleType = _db.VehicleType.Where(s => s.ID == car.VehicleTypeID).Select(s => s.TypeName).FirstOrDefault(),
                ColorID = car.ColorID,
                Color = _db.Color.Where(s => s.ID == car.ColorID).Select(s => s.ColorName).FirstOrDefault(),
                DriveTypeID = car.DriveTypeID,
                DriveType = _db.DriveType.Where(s => s.ID == car.DriveTypeID).Select(s => s.DriveTypeName).FirstOrDefault(),
                TransmissionID = car.TransmissionID,
                Transmission = _db.Transmission.Where(s => s.ID == car.TransmissionID).Select(s => s.TransmissionType).FirstOrDefault(),
                NumberOfSeats = car.NumberOfSeats,
                NumberOfDors = car.NumberOfDors,
                NumberOfGears = car.NumberOfGears,
                PowerPS = car.PowerPS,
                PowerKw = car.PowerKw,
                WheelSize = car.WheelSize,
                Ccm = car.Ccm,
                Kilometre = car.Kilometre,
                DateOfManufacture = car.DateOfManufacture.ToString("dd/MM.yyyy"),
                Price = _db.Car.Where(c => c.ID == car.ID).Select(c => c.Price).FirstOrDefault().ToString()
            };

            var carImageSET = _db.CarImage.Where(x => x.CarID == modelDetails.carID).ToList();
            var AllImages = new List<string>();

            foreach (var slika in carImageSET)
            {
                var ImageEntity = _db.Image.Where(i => i.ID == slika.ImageID).Select(i => i.PathToImage).FirstOrDefault();
                AllImages.Add(ImageEntity);
            }
            modelDetails.images = AllImages;


            return View(modelDetails);
        }
        public IActionResult AddToCart(int id)
        {
            var car = _db.Car
                 .Where(i => i.ID == id)
                 .SingleOrDefault();

            var claimsIdentiti = User.Identity as ClaimsIdentity;

            if (claimsIdentiti != null)
            {
                var userIdClaim = claimsIdentiti.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;

                    var item = new ShoppingCart
                    {
                        CarId = car.ID,
                        Car = car,
                        UserId = Int32.Parse(userIdValue),

                    };
                    _db.Add(item);
                    _db.SaveChanges();
                }
            }
            _nToastNotify.AddSuccessToastMessage("Added to cart");
            return Redirect("/Shop/CarDetails/ " + $"{id}");
        }
        public IActionResult ShoppingCart()
        {
            var claimsIdentiti = User.Identity as ClaimsIdentity;

            if (claimsIdentiti != null)
            {
                var userIdClaim = claimsIdentiti.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    List<ShoppingCartVM> cartModel = _db.ShoppingCart
                        .Where(i => i.UserId == Int32.Parse(userIdValue))
                        .Select(i => new ShoppingCartVM
                        {
                            CartId = i.Id,
                            CarId = i.CarId,
                            //pokusaj prebrojavanja istih auta u shoping cartu
                            CountSameCarID = _db.ShoppingCart.Where(c => i.CarId == c.CarId && c.UserId == i.UserId).Count(),
                            BrandId = i.Car.BrandID,
                            Brand = _db.Brand.Where(s => s.ID == i.Car.BrandID).Select(s => s.BrandName).FirstOrDefault(),
                            Model = i.Car.Model,
                            ModelId = 1,
                            Fuel = _db.Fuel.Where(s => s.ID == i.Car.FuelID).Select(s => s.FuelName).FirstOrDefault(),
                            FuelId = 1,
                            NumberOfDors = i.Car.NumberOfDors,
                            Price = _db.Car.Where(c => c.ID == i.CarId).Select(c => c.Price).FirstOrDefault()
                        }).ToList();

                    foreach (var item in cartModel)
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

                    return View(cartModel);
                }
            }
            return View();
        }
        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCart = _db.ShoppingCart.Where(i => i.Id == id).FirstOrDefault();
            _db.ShoppingCart.Remove(shoppingCart);
            _db.SaveChanges();
            _nToastNotify.AddSuccessToastMessage("Removed from cart");

            return Redirect("/Shop/ShoppingCart");
        }
        public IActionResult Checkout()
        {
            //List<Car> pomocna;
            var claimsIdentiti = User.Identity as ClaimsIdentity;
            if (claimsIdentiti != null)
            {
                var userIdClaim = claimsIdentiti.Claims
                   .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    int userIdValue = int.Parse(userIdClaim.Value);
                    var korisnik = _db.User.Where(S => S.Id == userIdValue).SingleOrDefault();
                    var shopcart = _db.ShoppingCart.Where(S => S.UserId == userIdValue).ToList();


                    List<ChackOutCarVM> ckmodelcar = _db.ShoppingCart.Where(i => i.UserId == userIdValue)
                       .Select(
                           i => new ChackOutCarVM
                           {
                               carID = i.CarId,
                               CountSameCarID = _db.ShoppingCart.Where(c => i.CarId == c.CarId).Count(),
                               Brand = _db.Brand.Where(s => s.ID == i.Car.BrandID).Select(s => s.BrandName).FirstOrDefault(),
                               Model = _db.Car.Where(s => s.ID == i.Car.ID).Select(s => s.Model).FirstOrDefault(),
                               Fuel = _db.Car.Where(s => s.ID == i.CarId).Select(s => s.Fuel.FuelName).FirstOrDefault(),
                               //TotalPrice += _db.Car.Where(c => c.ID == i.ID).Select(c => c.Price).FirstOrDefault()
                               Color = _db.Color.Where(s => s.ID == i.Car.ColorID).Select(s => s.ColorName).FirstOrDefault(),
                               DriveType = _db.DriveType.Where(s => s.ID == i.Car.DriveTypeID).Select(s => s.DriveTypeName).FirstOrDefault(),
                               Transmission = _db.Transmission.Where(s => s.ID == i.Car.TransmissionID).Select(s => s.TransmissionType).FirstOrDefault(),
                               PowerPS = i.Car.PowerPS,
                               PowerKw = i.Car.PowerKw,
                               DateOfManufacture = i.Car.DateOfManufacture.ToString("dd/MM/yyyy"),
                               DateofPurchase = DateTime.Now.ToString("dd/MM/yyyy"),
                               //TotalPrice += _db.Car.Where(s => s.ID == i.CarId).Select(s => s.Price).SingleOrDefault()
                               userid = userIdValue,
                               firstname = korisnik.FirstName,
                               lastname = korisnik.LastName,
                               username = korisnik.UserName,
                               email = korisnik.Email,
                               phonenumber = korisnik.PhoneNumber,
                           }).ToList();
                    return View(ckmodelcar);
                }
            }
            return View();
        }

        public PartialViewResult SearchCars(string searchtxt)
        {
            List<ShowCarsVM> model = _db.Car.Select(s => new ShowCarsVM
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

            int spaces = 0;
            int pozicija = 0;
            int duzina = 0;
            int spacecounter = 0;
            string result = null;
            int a = 0; 
            List<string> search = new List<string> { "", "", "", "", "" };
            if (searchtxt != null)
            {
                spaces = searchtxt.Count(Char.IsWhiteSpace);
                pozicija = searchtxt.IndexOf(' ');
                duzina = pozicija;
                result = null;
            }
            else 
                return PartialView("PomoćniSopViewDiv", model);

            if (spaces != 0)
            {

                for (int pocetak = 0; pocetak < searchtxt.Length;)
                {
                    result = searchtxt.Substring(pocetak, duzina);
                    search[a] = result;
                    a++;
                    pocetak = pozicija + 1;
                    spacecounter++;
                    duzina = pozicija;
                    if (spacecounter < spaces)
                    {
                        pozicija = searchtxt.IndexOf(' ', pocetak);
                        duzina = pozicija - duzina - 1;
                    }
                    else if (spacecounter == spaces)
                    {
                        duzina = searchtxt.Length - duzina - 1;
                    }
                    else
                    {
                        pocetak = searchtxt.Length + 1;
                    }
                }
            }
            else
            {
                search[0] = searchtxt;
            }
            if (searchtxt != null)
            {
                List<ShowCarsVM> rez1 = new List<ShowCarsVM>();
                rez1 = model.Where(i => search[0].ToLower().Contains(i.Brand.ToLower()) || i.Brand.ToLower().Contains(search[0].ToLower())
                                    || search[0].ToLower().Contains(i.CarModel.ToLower()) || i.CarModel.ToLower().Contains(search[0].ToLower())
                                    || search[0].ToLower().Contains(i.Fuel.ToLower()) || i.Fuel.ToLower().Contains(search[0].ToLower())
                                    || search[0].ToLower().Contains(i.Transmission.ToLower()) || i.Transmission.ToLower().Contains(search[0].ToLower())
                                    || search[0].ToLower().Contains(i.Color.ToLower()) || i.Color.ToLower().Contains(search[0].ToLower())).ToList();
                
                if (spaces < 1)
                    return PartialView("PomoćniSopViewDiv", rez1);
                List<ShowCarsVM> rez2 = new List<ShowCarsVM>();
                rez2 = rez1.Where(i => search[1].ToLower().Contains(i.Brand.ToLower()) || i.Brand.ToLower().Contains(search[1].ToLower())
                                    || search[1].ToLower().Contains(i.CarModel.ToLower()) || i.CarModel.ToLower().Contains(search[1].ToLower())
                                    || search[1].ToLower().Contains(i.Fuel.ToLower()) || i.Fuel.ToLower().Contains(search[1].ToLower())
                                    || search[1].ToLower().Contains(i.Transmission.ToLower()) || i.Transmission.ToLower().Contains(search[1].ToLower())
                                    || search[1].ToLower().Contains(i.Color.ToLower()) || i.Color.ToLower().Contains(search[1].ToLower())).ToList();

                if (spaces < 2)
                    return PartialView("PomoćniSopViewDiv", rez2);
                List<ShowCarsVM> rez3 = new List<ShowCarsVM>();
                rez3 = rez2.Where(i => search[2].ToLower().Contains(i.Brand.ToLower()) || i.Brand.ToLower().Contains(search[2].ToLower())
                                    || search[2].ToLower().Contains(i.CarModel.ToLower()) || i.CarModel.ToLower().Contains(search[2].ToLower())
                                    || search[2].ToLower().Contains(i.Fuel.ToLower()) || i.Fuel.ToLower().Contains(search[2].ToLower())
                                    || search[2].ToLower().Contains(i.Transmission.ToLower()) || i.Transmission.ToLower().Contains(search[2].ToLower())
                                    || search[2].ToLower().Contains(i.Color.ToLower()) || i.Color.ToLower().Contains(search[2].ToLower())).ToList();

                if (spaces < 3)
                    return PartialView("PomoćniSopViewDiv", rez3);
                List<ShowCarsVM> rez4 = new List<ShowCarsVM>();
                rez4 = rez3.Where(i => search[3].ToLower().Contains(i.Brand.ToLower()) || i.Brand.ToLower().Contains(search[3].ToLower())
                                    || search[3].ToLower().Contains(i.CarModel.ToLower()) || i.CarModel.ToLower().Contains(search[3].ToLower())
                                    || search[3].ToLower().Contains(i.Fuel.ToLower()) || i.Fuel.ToLower().Contains(search[3].ToLower())
                                    || search[3].ToLower().Contains(i.Transmission.ToLower()) || i.Transmission.ToLower().Contains(search[3].ToLower())
                                    || search[3].ToLower().Contains(i.Color.ToLower()) || i.Color.ToLower().Contains(search[3].ToLower())).ToList();

                if (spaces < 4)
                    return PartialView("PomoćniSopViewDiv", rez4);
                List<ShowCarsVM> rez5 = new List<ShowCarsVM>();
                rez5 = rez4.Where(i => search[4].ToLower().Contains(i.Brand.ToLower()) || i.Brand.ToLower().Contains(search[4].ToLower())
                                    || search[4].ToLower().Contains(i.CarModel.ToLower()) || i.CarModel.ToLower().Contains(search[4].ToLower())
                                    || search[4].ToLower().Contains(i.Fuel.ToLower()) || i.Fuel.ToLower().Contains(search[4].ToLower())
                                    || search[4].ToLower().Contains(i.Transmission.ToLower()) || i.Transmission.ToLower().Contains(search[4].ToLower())
                                    || search[4].ToLower().Contains(i.Color.ToLower()) || i.Color.ToLower().Contains(search[4].ToLower())).ToList();

                return PartialView("PomoćniSopViewDiv", rez5);
            }
            return PartialView("PomoćniSopViewDiv", model);
        }
    }
}
