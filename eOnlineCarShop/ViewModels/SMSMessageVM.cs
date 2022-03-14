using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class SMSMessageVM
    {
        [Display(Name = "Admin contact")]
        public List<SelectListItem> To { get; set; }
        [Display(Name = "Text message")]
        public string ContentMsg { get; set; }
        public string PhoneNumber { get; set; }
    }
}
