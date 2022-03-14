using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels.Service_ang
{
    public class ListCarServiceVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string ServiceName { get; set; }
            public string DateOfServiced { get; set; }
            public string DateofServiceWarranty { get; set; }
            public float Price { get; set; }
            public string CarName { get; set; }
        }
        public List<Row> list { get; set; }
        public int total { get; set; }
    }
}
