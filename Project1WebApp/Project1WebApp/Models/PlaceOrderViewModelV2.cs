using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1WebApp.Models
{
    public class PlaceOrderViewModelV2
    {
        public List<AvailInvViewModel> availInventory { get; set; }

        [Required]
        public Dictionary<int, int> custBought { get; set; }

        [Required]
        public int CustID { get; set; }

        [Required]
        public int LocID { get; set; }
    }
}