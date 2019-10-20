using System.Collections.Generic;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Interface that describes operations between the DB and the Business Logic
    /// </summary>
    public interface IRepository
    {
        public List<Customer> GetCustomers(string f, string l);

        public void AddCustomer(Customer c);

        public List<Inventory> GetInventory(int id);

        public List<Inventory> GetAvailInventory(int locationId);

        public void UpdateInventory(List<Inventory> i);

        public List<Location> GetLocations();

        public void AddOrder(Orders o);
        Location GetLocationByID(int id);
        List<Orders> GetLocationOrderHistory(int id);
        List<Orders> GetCustomerOrderHistory(int id);
        Customer GetCustomerById(int id);
        string GetProductNameById(int inventID);
        decimal GetProductPriceById(int inventID);
    }
}