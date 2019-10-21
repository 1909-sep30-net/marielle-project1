using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1WebApp.Models
{
    public class LocationOrdersViewModel
    {
        [DisplayName("Order Date")]
        public DateTime OrdDate { get; set; }
        public List<CustOrderViewModel> CustomerOrder { get; set; }
        [DisplayName("Customer Name")]
        public string CustName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        public int OrdID { get; set; }
    }
}