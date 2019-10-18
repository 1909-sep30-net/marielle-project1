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
            throw new System.NotImplementedException();
        }

        public List<BL.Inventory> GetAvailInventory()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}
