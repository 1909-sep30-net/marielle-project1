using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1WebApp.Models
{
    public class PlaceOrderViewModel
    {
        private int stock = 0;
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<int> Quantity { get; set; }

        public List<int> InvBought { get; set; }
        public int CustID { get; set; }
        public int LocID { get; set; }
        public int InvId { get; set; }
    }
}