using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_CS.EF_Models
{
    public class Role:IdentityRole<int>
    {
        public string RoleName { get; set; }
    }
}
