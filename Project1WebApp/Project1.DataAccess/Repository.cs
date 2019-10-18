using System.Collections.Generic;
using BL = Project1.BusinessLogic;
using Project1.DataAccess.Entities;
using System.Linq;

namespace Project1.DataAccess
{
    public class Repository : BL.IRepository
    {
        private readonly Project0DBContext _context;
        private DMapper _map;

        public Repository(Project0DBContext context)
        {
            _context = context;
            _map = new DMapper(context);
        }
        public void AddCustomer(BL.Customer c)
        {
            _context.Customer.Add(_map.ParseCustomer(c));
            _context.SaveChanges();
        }

        public void AddOrder(BL.Orders o)
        {
            UpdateInventory(o.CustOrder);
            _context.Orders.Add(_map.ParseOrders(o));
            _context.SaveChanges();

        }


        public List<BL.Inventory> GetAvailInventory(int locationId)
        {
            List<Inventory> dbAvail = _context.Inventory.Where(i => i.LocationId == locationId).ToList();
            List<BL.Inventory> availInv = new List<BL.Inventory>();
            foreach (Inventory i in dbAvail)
            {
                availInv.Add(_map.ParseInventory(i));
            }
            return availInv;
        }

        public BL.Customer GetCustomerById(int id)
        {
            return _map.ParseCustomer(_context.Customer.Single(c => c.CustId == id));

        }

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

        public List<BL.Customer> GetCustomers()
        {
            throw new System.NotImplementedException();
        }

        public List<BL.Customer> GetCustomers(string f, string l)
        {
            List <Customer> found = _context.Customer.Where(c => c.FirstName == f || c.LastName == l).ToList();
            List<BL.Customer> custFound = new List<BL.Customer>();
            foreach (Customer c in found)
            {
                custFound.Add(_map.ParseCustomer(c));
            }
            return custFound;
        }

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

        public BL.Location GetLocationByID(int id)
        {
            return _map.ParseLocation(_context.Location.Single(l => l.LocationId == id));
        }

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

        public List<BL.Location> GetLocations()
        {
            List<BL.Location> local = new List<BL.Location>();
            foreach (Location l in _context.Location.Select(l=>l).ToList())
            {
                local.Add(_map.ParseLocation(l));
            }
            return local;
        }

        public void UpdateInventory(List<BL.Inventory> i)
        {
            Inventory dbInv = new Inventory();
            foreach (BL.Inventory item in i)
            {
                dbInv = _context.Inventory.Single(inv => inv.InventoryId == item.InventID);
                dbInv.Stock = dbInv.Stock - item.Stock;
            }
            _context.SaveChanges();
        }
    }
}
