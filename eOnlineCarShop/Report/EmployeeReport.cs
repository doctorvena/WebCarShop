using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempReporTDesign
{
    public class EmployeeReport
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public static List<EmployeeReport> Get()
        {
            return new List<EmployeeReport> { };
        }
    }
}