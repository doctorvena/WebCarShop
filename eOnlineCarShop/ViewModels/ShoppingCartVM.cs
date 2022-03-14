using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class ShoppingCartVM
    {
        public int CartId { get; set; }
        public int CarId { get; set; }
        public int CountSameCarID { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public int ModelId { get; set; }
        public string Model { get; set; }
        public int FuelId { get; set; }
        public string Fuel { get; set; }
        public int NumberOfDors { get; set; }
        public List<string> images { get; set; }
        public float? Price { get; set; }

    }
}
