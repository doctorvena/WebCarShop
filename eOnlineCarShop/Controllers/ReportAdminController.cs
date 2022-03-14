using AspNetCore.Reporting;
using Data_CS.Data;
using Data_CS.EF_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempReporTDesign;

namespace eOnlineCarShop.Controllers
{
    public class ReportAdminController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<User> userManager;
        public ReportAdminController(ApplicationDbContext _db,UserManager<User> _userManager)
        {
            db = _db;
        }
        public static List<EmployeeReport> getZaposleni(ApplicationDbContext db,int userid)
        {
            List<EmployeeReport> podaci = db.User.Where(x => x.Id == userid).Select(y => new EmployeeReport
            {
                BirthDate = y.BirthDate.ToString("d"),
                City = y.City.Name,
                FirstName = y.FirstName,
                LastName = y.LastName,
                UserName = y.UserName,
                Gender = y.Gender.Name,
                PhoneNumber = y.PhoneNumber
            }).ToList();
            return podaci;
        }
        public IActionResult Index(int userid)
        {
            AspNetCore.Reporting.LocalReport _localReport = new AspNetCore.Reporting.LocalReport("Report/ReportEmployee.rdlc");
            List<EmployeeReport> podaci = getZaposleni(db, userid);
            _localReport.AddDataSource("DataSet1", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("IzdaoIzvjestaj", User.Identity.Name);

            ReportResult result = _localReport.Execute(RenderType.Pdf, parameters: parameters);
            return File(result.MainStream, "application/pdf");
        }
    }
}
