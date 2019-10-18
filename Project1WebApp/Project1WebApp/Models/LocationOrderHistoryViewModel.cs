using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class LocationOrderHistoryViewModel
    {
        public string BranchName { get; set; }
        public List<OrdersViewModel> Order { get; set; }
    }
}
