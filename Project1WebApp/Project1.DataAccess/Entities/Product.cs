using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class Product
    {
        public Product()
        {
            CustOrder = new HashSet<CustOrder>();
            Inventory = new HashSet<Inventory>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<CustOrder> CustOrder { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
