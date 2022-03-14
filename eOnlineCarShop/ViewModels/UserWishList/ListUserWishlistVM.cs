using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels.UserWishList
{
    public class ListUserWishlistVM
    {
        public class Row
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
        public List<Row> list { get; set; }
        public int total { get; set; }
        public string q { get; set; }
    }
}
