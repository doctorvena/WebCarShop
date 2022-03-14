using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace eOnlineCarShop.Controllers
{
    public class UserController : Controller
    {
       //[Authorize(Roles = "User")]

        private readonly UserManager<User> userManager;
        private ApplicationDbContext applicationDbContext;
        private readonly IToastNotification nToastNotify;

        public UserController(UserManager<User> _userManager, IToastNotification _nToastNotify, ApplicationDbContext _applicationDbContext)
        {
            userManager = _userManager;
            applicationDbContext = _applicationDbContext;
            nToastNotify = _nToastNotify;
        }
        public IActionResult UserInfo()
        {        
            return View();
        }

        [HttpGet]
        public IActionResult UserEdit()
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = applicationDbContext.User.Where(x => x.Id == id).Include(x => x.City).Include(x => x.Gender).FirstOrDefault();
            var model = new UserEditVM
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                CityID = user.CityID,
                City = applicationDbContext.City.Select(x => new SelectListItem { Value = x.CityID.ToString(), Text = x.Name }).ToList(),
                PhoneNumber = user.PhoneNumber,
                GenderID = user.GenderID,
                Gender = applicationDbContext.Gender.Select(x => new SelectListItem { Value = x.GenderID.ToString(), Text = x.Name }).ToList()

            };

            return View(model);
        }
        [HttpPost]
        public IActionResult UserEdit(UserEditVM model)
        {
            var user = applicationDbContext.User.Find(model.Id);
            if (ModelState.IsValid)
            {

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDate;
                user.CityID = model.CityID;
                user.PhoneNumber = model.PhoneNumber;
                user.GenderID = model.GenderID;
                applicationDbContext.SaveChanges();
                nToastNotify.AddSuccessToastMessage("Successfully updated");
                return RedirectToAction("UserInfo", "User");

            }
            else
            {
                model = new UserEditVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    CityID = user.CityID,
                    City = applicationDbContext.City.Select(x => new SelectListItem { Value = x.CityID.ToString(), Text = x.Name }).ToList(),
                    PhoneNumber = user.PhoneNumber,
                    GenderID = user.GenderID,
                    Gender = applicationDbContext.Gender.Select(x => new SelectListItem { Value = x.GenderID.ToString(), Text = x.Name }).ToList()
                };
            }
            return View(model);
        }
    }
}