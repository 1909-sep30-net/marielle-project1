using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Location format of the program
    /// UI interacts with this format of location
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Branch Name of Location 
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Store inventory (List of products with their available stock)
        /// </summary>

        public List<Inventory> Inventory { get; set; }

        /// <summary>
        /// Address of Location
        /// </summary>
        public string Street { get; set; }

        public string City { get; set; }

        public States State { get; set; }

        public int Zipcode { get; set; }

        public int LocID { get; set; }

    }
}
