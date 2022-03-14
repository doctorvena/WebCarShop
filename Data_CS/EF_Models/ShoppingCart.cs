using System;
using System.Collections.Generic;
using System.Text;

namespace Data_CS.EF_Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int UserId { get; set; }
    }
}
