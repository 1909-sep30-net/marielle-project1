using Project1.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using BL = Project1.BusinessLogic;

namespace Project1.DataAccess
{
    /// <summary>
    /// Class that contains methods concerned with operations on the DB
    /// </summary>
    public class Repository : BL.IRepository
    {
        /// <summary>
        /// Context needed to connect with the DB
        /// </summary>
        private readonly Project0DBContext _context;
        /// <summary>
        /// Mapper needed to map betweeen Business Logic objects and DB objects
        /// </summary>
        private IMapper _map;

        public Repository(Project0DBContext context)
        {
            _context = context;
            _map = new DMapper(context);
        }
        /// <summary>
        /// Method that adds customer to the database
        /// </summary>
        /// <param name="c"></param>
        public void AddCustomer(BL.Customer c)
        {
            _context.Customer.Add(_map.ParseCustomer(c));
            _context.SaveChanges();
        }
        /// <summary>
        /// Method to add order to DB
        /// </summary>
        /// <param name="o"></param>
        public void AddOrder(BL.Orders o)
        {
            if (o.CustOrder.Count < 1) throw new InvalidQuantityException("Must buy something");
            UpdateInventory(o.CustOrder);
            _context.Orders.Add(_map.ParseOrders(o));
            _context.SaveChanges();
        }
        /// <summary>
        /// Method that returns the available inventory
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>

        public List<BL.Inventory> GetAvailInventory(int locationId)
        {
            List<Inventory> dbAvail = _context.Inventory.Where(i => i.LocationId == locationId).ToList();
            List<BL.Inventory> availInv = new List<BL.Inventory>();
            foreach (Inventory i in dbAvail)
            {
                if (i.Stock > 0) availInv.Add(_map.ParseInventory(i));
            }
            return availInv;
        }
        /// <summary>
        /// Method that returns customer from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BL.Customer GetCustomerById(int id)
        {
            return _map.ParseCustomer(_context.Customer.Single(c => c.CustId == id));
        }

        /// <summary>
        /// Method that gets order history of customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BL.Orders> GetCustomerOrderHistory(int id)
        {
            List<BL.Orders> custOrder = new List<BL.Orders>();
            List<Orders> dbCustOrder = _context.Orders.Where(o => o.CustId == id).ToList();
            foreach (Orders o in dbCustOrder)
            {
                custOrder.Add(_map.ParseOrders(o));
            }
            return custOrder;
        }
        /// <summary>
        /// Method that gets customers matching search parameters
        /// </summary>
        /// <param name="f"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public List<BL.Customer> GetCustomers(string f, string l)
        {
            List<Customer> found = _context.Customer.Where(c => c.FirstName == f || c.LastName == l).ToList();
            List<BL.Customer> custFound = new List<BL.Customer>();
            foreach (Customer c in found)
            {
                custFound.Add(_map.ParseCustomer(c));
            }
            return custFound;
        }
        /// <summary>
        /// Method that returns inventory of Location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BL.Inventory> GetInventory(int id)
        {
            List<BL.Inventory> inv = new List<BL.Inventory>();
            List<Inventory> localInv = _context.Inventory.Where(i => i.LocationId == id).ToList();
            foreach (Inventory i in localInv)
            {
                inv.Add(_map.ParseInventory(i));
            }
            return inv;
        }
        /// <summary>
        /// Method that returns location 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BL.Location GetLocationByID(int id)
        {
            return _map.ParseLocation(_context.Location.Single(l => l.LocationId == id));
        }
        /// <summary>
        /// Method that returns order history of a location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BL.Orders> GetLocationOrderHistory(int id)
        {
            List<BL.Orders> locOrdHist = new List<BL.Orders>();
            List<Orders> dbOrdHist = _context.Orders.Where(o => o.LocationId == id).ToList();
            foreach (Orders item in dbOrdHist)
            {
                locOrdHist.Add(_map.ParseOrders(item));
            }
            return locOrdHist;
        }
        /// <summary>
        /// Method that returns all available locations
        /// </summary>
        /// <returns></returns>
        public List<BL.Location> GetLocations()
        {
            List<BL.Location> local = new List<BL.Location>();
            foreach (Location l in _context.Location.Select(l => l).ToList())
            {
                local.Add(_map.ParseLocation(l));
            }
            return local;
        }
        /// <summary>
        /// Method that returns product name
        /// </summary>
        /// <param name="inventID"></param>
        /// <returns></returns>
        public string GetProductNameById(int inventID)
        {
            return _context.Product.Single(p => p.ProductId == _context.Inventory.Single(inv => inv.InventoryId == inventID).ProductId).Name;
        }
        /// <summary>
        /// Method that returns price of product
        /// </summary>
        /// <param name="inventID"></param>
        /// <returns></returns>
        public decimal GetProductPriceById(int inventID)
        {
            return _context.Product.Single(p => p.ProductId == _context.Inventory.Single(inv => inv.InventoryId == inventID).ProductId).Price;
        }

        

        /// <summary>
        /// Method that updates the inventory upon placing an order
        /// </summary>
        /// <param name="i"></param>
        public void UpdateInventory(List<BL.Inventory> i)
        {
            Inventory dbInv = new Inventory();
            foreach (BL.Inventory item in i)
            {
                if (item.Stock < 1) throw new InvalidQuantityException("Quantity must be greater than 0");
                dbInv = _context.Inventory.Single(inv => inv.InventoryId == item.InventID);
                
                if (dbInv.Stock < item.Stock) throw new StockInsufficientException("Stock Insufficient");
                dbInv.Stock = dbInv.Stock - item.Stock;
            }
            _context.SaveChanges();
        }
    }
}