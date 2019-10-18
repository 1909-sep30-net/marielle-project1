using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLogic
{
    public class Orders
    {
        public Customer Cust { get; set; }
        public Location Location { get; set; }

        public List<Inventory> CustOrder { get; set; }

        public decimal Total { get; set; }
        public DateTime Date { get; set; }

        public int OrdID { get; set; }
    }
}
