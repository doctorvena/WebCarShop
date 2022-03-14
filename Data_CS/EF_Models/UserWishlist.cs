using System;
using System.Collections.Generic;
using System.Text;

namespace Data_CS.EF_Models
{
    public class UserWishlist
    {
        public int id { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string fuel { get; set; }
        public string color { get; set; }
        public string numberOfGears { get; set; }
        public int powerKw { get; set; }
        public float wheelSize { get; set; }
    }
}
