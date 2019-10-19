using System.ComponentModel;

namespace Project1WebApp.Models
{
    public class InventoryViewModel
    {
        [DisplayName("Branch Name")]
        public string BanchName { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public int Stock { get; set; }

        public int InventID { get; set; }
    }
}