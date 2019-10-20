using Project1.DataAccess.Entities;
using BL = Project1.BusinessLogic;
namespace Project1.DataAccess
{
    /// <summary>
    /// Interface that describes the mappiing between business logic objects to DB objects and vice versa
    /// </summary>
    internal interface IMapper
    {
        public BL.Customer ParseCustomer(Customer c);
        public Customer ParseCustomer(BL.Customer c);

        public BL.Inventory ParseInventory(Inventory i);

        public BL.Location ParseLocation(Location l);
        

        public BL.Orders ParseOrders(Orders o);
        public Orders ParseOrders(BL.Orders o);

        public BL.Product ParseProduct(Product p);
        
    }
}