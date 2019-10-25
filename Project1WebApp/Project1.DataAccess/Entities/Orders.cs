using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            CustOrder = new HashSet<CustOrder>();
        }

        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public int? CustId { get; set; }
        public int? LocationId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<CustOrder> CustOrder { get; set; }
    }
}