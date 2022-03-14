using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using eOnlineCarShop.ViewModels.Service_ang;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    [Produces("application/json")]
    public class ServiceController : Controller
    {
        private ApplicationDbContext ctx;
        public ServiceController(ApplicationDbContext _ctx)
        {
            ctx = _ctx;
        }
        [HttpGet]
        public IActionResult Prikaz([FromBody] string q, int currentPage = 1, int itemsPerPage = 5)
        {
            IQueryable<ServicedCars> lista;

            lista = ctx.ServicedCars.Where(a => q == null || (a.Car.Model).StartsWith(q) || (a.Car.Model).StartsWith(q));

            ListCarServiceVM model = new ListCarServiceVM
            {
                list = lista.Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(s => new ListCarServiceVM.Row
                {
                    ID = s.ID,
                    ServiceName = s.ServiceName,
                    DateOfServiced = s.DateOfServiced.ToString(),
                    DateofServiceWarranty = s.DateofServiceWarranty.ToString(),
                    CarName = s.Car.Model,
                    Price = s.Price
                }).ToList(),
                total = lista.Count()
            };
            return Ok(model);
        }
        /*   public async Task<IActionResult> GetServicedCars()
           {
               List<DetailsCarServiceVM> model = new List<DetailsCarServiceVM>();
               await
               foreach (var cars in ctx.ServicedCars)
               {
                   model.Add(new DetailsCarServiceVM
                   {
                       ID = cars.ID,

                   });
               }
               return Ok(model);
           }*/



        public IActionResult Edit(int ID)
        {
            ServicedCars servis = ctx.ServicedCars.Where(x => x.ID == ID).Include(x => x.Car).FirstOrDefault();

            EditCarServiceVM model = new EditCarServiceVM
            {
                id = servis.ID,
                serviceName = servis.ServiceName,
                dateOfServiced = servis.DateOfServiced,
                selectedCar = servis.Car.Model,// onemogucit uredjivanje 
                price = servis.Price,
                recommendations = servis.Recommendations,
                warnings = servis.Warnings,
                dateofServiceWarranty = servis.DateofServiceWarranty
            };
            return Ok(model);
        }
        [HttpGet]
        public IActionResult Details(int ID) // vraca detalje 
        {
            ServicedCars servis = ctx.ServicedCars.Where(x => x.ID == ID).Include(x => x.Car).FirstOrDefault();

            DetailsCarServiceVM model = new DetailsCarServiceVM
            {
                ServiceName = servis.ServiceName,
                DateOfServiced = servis.DateOfServiced.ToString(),
                SelectedCar = servis.Car.Model,
                Price = servis.Price,
                Recommendations = servis.Recommendations,
                Warnings = servis.Warnings,
                DateofServiceWarranty = servis.DateofServiceWarranty.ToString()
            };
            return Ok(model);
        }
        // obrisi servis
        public IActionResult Delete(int ID)
        {
            ServicedCars servis = ctx.ServicedCars.Find(ID);
            ctx.Remove(servis);
            ctx.SaveChanges();

            return Ok();
        }
        [HttpPost]
        public IActionResult EditSave([FromBody] EditCarServiceVM model)
        {
            ServicedCars servis = ctx.ServicedCars.Where(x => x.ID == model.id).Include(x => x.Car).FirstOrDefault();

            servis.DateOfServiced = model.dateOfServiced;
            servis.Recommendations = model.recommendations;
            servis.Warnings = model.warnings;
            servis.Price = model.price;
            servis.ServiceName = model.serviceName;
            ctx.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IActionResult AddService()
        {
            AddCarServiceVM model = new AddCarServiceVM
            {
                DateOfServiced = DateTime.Now,
                Cars = ctx.Car.Select(x => new SelectListItem
                {
                    Text = x.Model.ToString(),
                    Value = x.ID.ToString()
                }).ToList()
            };
            return Ok(model);
        }
        public IActionResult AddSave([FromBody] AddCarServiceVM model)
        {
            int carID = Int32.Parse(model.SelectedCar);

            ServicedCars novi = new ServicedCars
            {
                ServiceName = model.ServiceName,
                Description = model.Description,
                DateOfServiced = model.DateOfServiced,
                DateofServiceWarranty = model.DateofServiceWarranty,
                CarID = carID,
                Price = model.Price,
                Recommendations = model.Recommendations,
                Warnings = model.Warnings
            };
            ctx.Add(novi);
            ctx.SaveChanges();
            return Ok();
        }
    }
}
