using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class CustomerOrdersViewModel
    {
        public DateTime OrdDate { get; set; }
        public List<CustOrderViewModel> CustomerOrder { get; set; }

        public string BranchName { get; set; }
        public decimal Total { get; set; }
        public int OrdID { get; set; }
    }
}
