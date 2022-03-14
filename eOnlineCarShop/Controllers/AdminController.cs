using Data_CS.Data;
using Data_CS.EF_Models;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AdminController(ApplicationDbContext _applicationDbContext, UserManager<User> _userManager,
            SignInManager<User> _signInManager)
        {
            applicationDbContext = _applicationDbContext;
            userManager = _userManager;
            signInManager = _signInManager;
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
                            return RedirectToAction("AdminHome", "Administrator");
                        else
                            return RedirectToAction("Index", "Home");
                    }

                ModelState.AddModelError("", "Sorry , try again!");
            }
            return View(model);
        }


        public async Task<IActionResult> Index()
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
            return Ok(model);
        }
    }
}
