using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class InventoryViewModel
    {
        public string BanchName { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public int Stock { get; set; }

        public int InventID { get; set; }
    }
}
