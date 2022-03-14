using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confrim Password")]
        [Compare("Password", ErrorMessage = "Try again!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        public int CityID { get; set; }
        public List<SelectListItem> city { get; set; }

        [Display(Name = "Adress")]
        public string Adress { get; set; }

        [Phone]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "BirthDate")]
        public DateTime BirthDate { get; set; }

        public int GenderID { get; set; }
        public List<SelectListItem> genders { get; set; }
       
    }
}
