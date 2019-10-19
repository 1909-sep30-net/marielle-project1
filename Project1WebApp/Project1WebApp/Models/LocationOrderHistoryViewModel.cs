using System.Collections.Generic;
using System.ComponentModel;

namespace Project1WebApp.Models
{
    public class LocationOrderHistoryViewModel
    {
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        public List<LocationOrdersViewModel> Order { get; set; }
    }
}