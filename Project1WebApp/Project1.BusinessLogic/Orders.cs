using System;
using System.Collections.Generic;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Order format of the program
    /// UI interacts with this format of the Order
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// Customer who placed the order
        /// </summary>
        public Customer Cust { get; set; }

        /// <summary>
        /// Location where order was placed
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// List of products with their corresponding quantities that a customer ordered
        /// </summary>
        public List<Inventory> CustOrder { get; set; }

        /// <summary>
        /// Total of an order
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Time and Date when order was placed
        /// </summary>
        public DateTime Date { get; set; }

        public int OrdID { get; set; }
    }
}