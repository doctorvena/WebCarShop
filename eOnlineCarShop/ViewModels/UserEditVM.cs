using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class UserEditVM
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public int CityID { get; set; }
        public List<SelectListItem> City { get; set; }
        public string Adress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public int GenderID { get; set; }
        public List<SelectListItem> Gender { get; set; }
    }
}
