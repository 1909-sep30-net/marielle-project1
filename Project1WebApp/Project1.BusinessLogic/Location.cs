using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLogic
{
    public class Location
    {
        public string BranchName { get; set; }

        public List<Inventory> Inventory { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public States State { get; set; }

        public int Zipcode { get; set; }

        public int LocID { get; set; }

    }
}
