using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<Role> roleManager;
        private readonly IToastNotification nToastNotify;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext applicationDbContext,
            RoleManager<Role> roleManager, IToastNotification _nToastNotify)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            this.roleManager = roleManager;
            nToastNotify = _nToastNotify;
            //this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterVM
            {
                city = _applicationDbContext.City.Select(x =>  new SelectListItem { Value = x.CityID.ToString(), Text = x.Name })
                .ToList(),
                genders = _applicationDbContext.Gender.Select(x => new SelectListItem { Value = x.GenderID.ToString(), Text = x.Name })
                .ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
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
                    await signInManager.SignInAsync(user, isPersistent: false);        
                    await userManager.AddToRoleAsync(user, "Client");
                    return RedirectToAction("Index", "Home");
             
                }
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
            }
            else
            {
                model = new RegisterVM
                {
                    city = _applicationDbContext.City.Select(x => new SelectListItem { Value = x.CityID.ToString(), Text = x.Name })
          .ToList(),
                    genders = _applicationDbContext.Gender.Select(x => new SelectListItem { Value = x.GenderID.ToString(), Text = x.Name })
          .ToList()
                };
            }
            return View(model);
        }
    
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        nToastNotify.AddWarningToastMessage("You successfully logout!");
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);//provjera
                if (result.Succeeded)
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    else
                    {
                        var user = await userManager.FindByNameAsync(model.Email);
                        if (await userManager.IsInRoleAsync(user, "Client"))
                            return RedirectToAction("Index", "Home");
                        //else if (await userManager.IsInRoleAsync(user, "Employee"))
                        //    return RedirectToAction("Index", "Employee");
                        else if (await userManager.IsInRoleAsync(user, "Administrator"))
                        { nToastNotify.AddSuccessToastMessage("You successfully login, wellcome admin" + model.Email);
                            return RedirectToAction("AdminHome", "Administrator");
                        }
                        else
                            return RedirectToAction("Index", "Home");
                    }

                ModelState.AddModelError("", "Sorry , try again!");
            }
            return View(model);
        }
    }
}