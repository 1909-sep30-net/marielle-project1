using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class CustomerOrdersViewModel
    {
        [DisplayName("Order Date")]
        public DateTime OrdDate { get; set; }

        public List<CustOrderViewModel> CustomerOrder { get; set; }

        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        public int OrdID { get; set; }
    }
}
