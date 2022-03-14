using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eOnlineCarShop.Models;
using Data_CS.Data;
using Data_CS.EF_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Globalization;

namespace eOnlineCarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        public readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db, UserManager<User> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Chat()
        {
            List<Message> messages = _db.Messages.ToList();
            return View(messages);
        }
        public async Task<IActionResult> Chat2()
        {
            List<Message> messages = _db.Messages.ToList();
            return View(messages);
        }
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.Username = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserID = sender.Id.ToString();
                await _db.Messages.AddAsync(message);
                await _db.SaveChangesAsync();
                return Redirect("/Home/Chat2");
            }
            return Redirect("/Home/Chat2");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
