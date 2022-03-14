using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempReportVena
{
    public class ReportCheckout
    {
        //public int ChackOutCarID { get; set; }
        public int carID { get; set; }
        public int CountSameCarID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public float? Price { get; set; }
        public float? TotalPrice { get; set; }


        //public int ChackOutUserID { get; set; }
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }

        public static List<ReportCheckout> Get()
        {
            return new List<ReportCheckout> { };
        }
    }
}