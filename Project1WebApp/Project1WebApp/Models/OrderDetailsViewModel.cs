using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class OrderDetailsViewModel
    {
        public List<AvailInvViewModel> custBought { get; set; }
        public string CustomerName { get; set; }
        public string LocationName { get; set; }
    }
}
