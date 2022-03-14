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
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly UserManager<User> userManager;
        //private readonly SignInManager<User> signInManager;
        private ApplicationDbContext applicationDbContext;
        //private readonly RoleManager<Role> roleManager;
        private readonly IToastNotification nToastNotify;
        public AdministratorController(UserManager<User> _userManager, IToastNotification _nToastNotify, ApplicationDbContext _applicationDbContext)
        {
            userManager = _userManager;
            //signInManager = _signInManager;
            applicationDbContext = _applicationDbContext;
            nToastNotify = _nToastNotify;
        }
        public IActionResult AdminHome()
        {
            return View();
        }
        public async Task<IActionResult> EmployeeList(int? page)
        {
            List<UserVM> model = new List<UserVM>();
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, "Employee"))
                {
                    model.Add(new UserVM
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,  
                        LastName = user.LastName
                    });
                }

            }
            IPagedList<UserVM> lista = model.ToPagedList(page ?? 1, 3);
            return View(lista);
        }
        public IActionResult EmployeeDetails(int id)
        {

            var user = applicationDbContext.User.Where(x => x.Id == id).Include(x => x.Gender).Include(x => x.City).FirstOrDefault();
            UserDetailVM model = new UserDetailVM
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                CityName = applicationDbContext.City.Find(user.CityID).Name,
                Gender = applicationDbContext.Gender.Find(user.GenderID).Name,
                Email = user.Email,

            };
            nToastNotify.AddInfoToastMessage("Employee details about: " + model.FirstName);
            return View(model);
        }
        [HttpGet]
        public IActionResult AdminEdit()
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
        public IActionResult AdminEdit(UserEditVM model)
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
                nToastNotify.AddSuccessToastMessage("You update your profile date!");
                return RedirectToAction("AdminHome", "Administrator");
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

        [HttpGet]
        public IActionResult EmployeeAdd()
        {
            var model = new RegisterVM
            {
                city = applicationDbContext.City.Select(x => new SelectListItem { Value = x.CityID.ToString(), Text = x.Name })
                .ToList(),
                genders = applicationDbContext.Gender.Select(x => new SelectListItem { Value = x.GenderID.ToString(), Text = x.Name })
                .ToList()
            };
         
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeAdd(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    CityID = model.CityID,
                    PhoneNumber = model.PhoneNumber,
                    GenderID = model.GenderID

                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    nToastNotify.AddSuccessToastMessage("You successfully add " + model.FirstName);
                    await userManager.AddToRoleAsync(user, "Employee");
                    return RedirectToAction("EmployeeList", "Administrator");

                }
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
            }
            else
            {
                model = new RegisterVM
                {
                    city = applicationDbContext.City.Select(x => new SelectListItem { Value = x.CityID.ToString(), Text = x.Name })
      .ToList(),
                    genders = applicationDbContext.Gender.Select(x => new SelectListItem { Value = x.GenderID.ToString(), Text = x.Name })
      .ToList()
                };
            }
   
            return View(model);
        }
        public IActionResult EmployeeEdit (int id)
        {
            var user = applicationDbContext.User.Find(id);
            var model = new UserEditVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                CityID=user.CityID,
                GenderID=user.GenderID,
                City = applicationDbContext.City.Select(x => new SelectListItem { Value = x.CityID.ToString(), Text = x.Name })
                .ToList(),
                Gender= applicationDbContext.Gender.Select(x => new SelectListItem { Value = x.GenderID.ToString(), Text = x.Name })
                .ToList()
            };
            return View(model);
        }
         public IActionResult EmployeeChangeSave(UserEditVM user)
        {
            if (ModelState.IsValid)
            {
                var user1 = applicationDbContext.User.Find(user.Id);
                user1.FirstName = user.FirstName;
                user1.LastName = user.LastName;
                user1.BirthDate = user.BirthDate;
                user1.CityID = user.CityID;
                user1.PhoneNumber = user.PhoneNumber;
                user1.GenderID = user.GenderID;

                applicationDbContext.SaveChanges();
                nToastNotify.AddSuccessToastMessage("You successfully edit " + user1.FirstName);
                return RedirectToAction("EmployeeList", "Administrator");
            }
            else
            {
           
                nToastNotify.AddErrorToastMessage("Try again!");
                return RedirectToAction("EmployeeEdit", "Administrator", new { id = user.Id });
            }
    
        }
        public async Task<IActionResult> EmployeeRemove(int id)
        {
            var user = applicationDbContext.User.Find(id);
            await userManager.RemoveFromRoleAsync(user, "Employee");
           await userManager.AddToRoleAsync(user, "Client");
            nToastNotify.AddErrorToastMessage("You remove " + user.FirstName);
            return RedirectToAction("EmployeeList", "Administrator");
        }
    }
}
