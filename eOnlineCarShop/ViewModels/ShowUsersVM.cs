using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class ShowUsersVM
    {
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string birthdate { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool emailconfirmed { get; set; }
        public string phonenumber { get; set; }
        public bool phonenumberconfirmed { get; set; }
    }
}
