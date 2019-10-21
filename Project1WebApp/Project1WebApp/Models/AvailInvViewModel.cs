using System.ComponentModel.DataAnnotations;

namespace Project1WebApp.Models
{
    public class AvailInvViewModel
    {
        public string ProductName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
        public int InventID { get; set; }
    }
}