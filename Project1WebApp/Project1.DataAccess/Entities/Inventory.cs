﻿namespace Project1.DataAccess.Entities
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}