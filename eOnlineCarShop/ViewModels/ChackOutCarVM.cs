using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class ChackOutCarVM
    {
        public int ChackOutCarID { get; set; }
        public int carID { get; set; }
        public int CountSameCarID{get;set;}
        public string Brand { get; set; }
        public string Model { get; set; }
        public int FuelID { get; set; }
        public string Fuel { get; set; }
        public int ColorID { get; set; }
        public string Color { get; set; }
        public int DriveTypeID { get; set; }
        public string DriveType { get; set; }
        public int TransmissionID { get; set; }
        public string Transmission { get; set; }
        public int PowerPS { get; set; }
        public int PowerKw { get; set; }
        public string DateOfManufacture { get; set; }
        public string DateofPurchase { get; set; }
        public float? TotalPrice { get; set; }


        public int ChackOutUserID { get; set; }
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
    }
}
