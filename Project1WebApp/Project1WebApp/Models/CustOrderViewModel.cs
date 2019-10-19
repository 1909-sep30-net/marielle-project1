using System.ComponentModel.DataAnnotations;

namespace Project1WebApp.Models
{
    public class CustOrderViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

    }
}