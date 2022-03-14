using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_CS.EF_Models
{
    public class User:IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }
        
        [ForeignKey("Gender")]
        public int GenderID { get; set; }
        public Gender Gender { get; set; }
  
    }
}
