using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class CustOrder
    {
        public int CustOrderId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
