using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class Location
    {
        public Location()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
        }

        public int LocationId { get; set; }
        public string BranchName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
