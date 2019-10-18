using Project1.DataAccess.Entities;
using BL = Project1.BusinessLogic;
namespace Project1.DataAccess
{
    internal interface IMapper
    {
        public BL.Customer ParseCustomer(Customer c);
        public Customer ParseCustomer(BL.Customer c);

        public BL.Inventory ParseInventory(Inventory i);
        public Inventory ParseInventory(BL.Inventory i);

        public BL.Location ParseLocation(Location l);
        public Location ParseLocation(BL.Location l);

        public BL.Orders ParseOrders(Orders o);
        public Orders ParseOrders(BL.Orders o);

        public BL.Product ParseProduct(Product p);
        public Product ParseProduct(BL.Product p);
        
    }
}