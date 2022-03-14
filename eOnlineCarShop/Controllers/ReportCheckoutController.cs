using AspNetCore.Reporting;
using Data_CS.Data;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
//using eOnlineCarShop.Report;
using TempReportVena;

namespace eOnlineCarShop.Controllers
{
    public class ReportCheckoutController : Controller
    {
        private ApplicationDbContext _db;
        private readonly IToastNotification _nToastNotify;
        public ReportCheckoutController(ApplicationDbContext db, IToastNotification nToastNotify)
        {
            _db = db;
            _nToastNotify = nToastNotify;
        }
        public static List<ReportCheckout> DodajPodatke(ApplicationDbContext db,int userID)
        {
            var autaid = db.ShoppingCart.Where(s=>s.UserId==userID).Select(s=>s.CarId).ToList();

            //List<ReportCheckout> podaci = null;

            //foreach (var item in autaid)
            //{
            //    podaci = db.Car.Where(s => s.ID == item).Select(s => new ReportCheckout
            //    {
            //        carID = s.ID,
            //        Brand = s.brand.BrandName,
            //        Model = s.Model,
            //        Price = s.Price,
            //        userid = userID,
            //        firstname = db.User.Where(c => c.Id == userID).Select(c => c.FirstName).FirstOrDefault(),
            //        lastname = db.User.Where(c => c.Id == userID).Select(c => c.LastName).FirstOrDefault(),
            //        username = db.User.Where(c => c.Id == userID).Select(c => c.UserName).FirstOrDefault(),
            //        email = db.User.Where(c => c.Id == userID).Select(c => c.Email).FirstOrDefault(),
            //        phonenumber = db.User.Where(c => c.Id == userID).Select(c => c.PhoneNumber).FirstOrDefault(),
            //        TotalPrice = s.Price
            //    }).ToList();
            //}

            List<ReportCheckout> podaci= db.Car.Where(s => autaid.Contains(s.ID)).Select(s => new ReportCheckout
            {
                carID = s.ID,
                Brand = s.brand.BrandName,
                Model = s.Model,
                Price = s.Price,
                CountSameCarID= db.ShoppingCart.Where(c => s.ID == c.CarId).Count(),
                userid = userID,
                firstname = db.User.Where(c => c.Id == userID).Select(c => c.FirstName).FirstOrDefault(),
                lastname = db.User.Where(c => c.Id == userID).Select(c => c.LastName).FirstOrDefault(),
                username = db.User.Where(c => c.Id == userID).Select(c => c.UserName).FirstOrDefault(),
                email = db.User.Where(c => c.Id == userID).Select(c => c.Email).FirstOrDefault(),
                phonenumber = db.User.Where(c => c.Id == userID).Select(c => c.PhoneNumber).FirstOrDefault(),
                TotalPrice = s.Price
            }).ToList();

            float? pomocna = 0;
            foreach (var item in podaci)
            {             
                if(item.Price!=null)
                    pomocna += item.Price*item.CountSameCarID;
            }
            foreach (var item in podaci)
            {
                item.TotalPrice = pomocna;
            }
            return podaci;
        }
        
        public IActionResult Print()
        {
            int userID = -1;
            var claimsIdentiti = User.Identity as ClaimsIdentity;

            if (claimsIdentiti != null)
            {
                var userIdClaim = claimsIdentiti.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    userID = Int32.Parse(userIdValue);
                }
            }

            LocalReport localReport = new LocalReport("Report/ReportCheckout.rdlc");
            var podaci  = DodajPodatke(_db, userID);
            localReport.AddDataSource("DataSet1", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportMadeBy", "eOnlineCarShop");
            parameters.Add("Date", DateTime.Now.ToString("MMM dd yyyy"));

            ReportResult resul = localReport.Execute(RenderType.Pdf, parameters: parameters);
            return File(resul.MainStream, "application/pdf");
            //return Redirect("/Shop/Checkout");
        }
    }
}
