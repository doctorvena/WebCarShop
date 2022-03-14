using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    public class SMSController : Controller
    {
        private ApplicationDbContext ctx;
        private readonly UserManager<User> userManager;
        public SMSController(ApplicationDbContext _ctx, UserManager<User> _userManager)
        {
            ctx = _ctx;
            userManager = _userManager;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            List<User> list = new List<User>();
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, "Administrator"))
                {
                    list.Add(user);
                }
            }

            SMSMessageVM model = new SMSMessageVM
            {
                To = list.Select(x => new SelectListItem
                {
                    Text = x.PhoneNumber.ToString() + "   " + x.FirstName,
                    Value = x.PhoneNumber.ToString()
                }).ToList(),
                
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SendMessage(SMSMessageVM message)
        {
            
            var results = SMS.Send(new SMS.SMSRequest
            {
                from = "Your mobile shop" /*Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"]*/,
                to = message.PhoneNumber,
                text = message.ContentMsg
            });
            return RedirectToAction("EmployeeList", "Administrator");
        }
    }
}
