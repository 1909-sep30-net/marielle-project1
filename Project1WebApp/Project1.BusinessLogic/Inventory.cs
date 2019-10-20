namespace Project1.BusinessLogic
{
    /// <summary>
    /// Defines what is in inventory
    /// Order UI uses in location inventory and customer order
    /// </summary>
    public class Inventory
    {
        public Product Prod { get; set; }
        /// <summary>
        /// Property that describes either how much a customer wants to buy in a customer order
        /// or how much stock is left in a certian location
        /// </summary>
        public int Stock { get; set; }
        public int InventID { get; set; }
    }
}