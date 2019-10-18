using System;
using System.Collections.Generic;

namespace Project1WebApp.Models
{
    public class LocationOrdersViewModel
    {
        public DateTime OrdDate { get; set; }
        public List<CustOrderViewModel> CustomerOrder { get; set; }

        public string CustName { get; set; }
        public decimal Total { get; set; }
        public int OrdID { get; set; }
    }
}