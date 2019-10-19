using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class OrderDetailsViewModel
    {
        public List<AvailInvViewModel> custBought { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [DisplayName("Branch Name")]
        public string LocationName { get; set; }
    }
}
