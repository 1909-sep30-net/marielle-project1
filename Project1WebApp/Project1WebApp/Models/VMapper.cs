using Project1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class VMapper
    {
        public Customer ParseCustomer(CustomerViewModel viewmodel)
        {
            return new Customer()
            {
                City = viewmodel.City,
                Street = viewmodel.Street,
                FirstName = viewmodel.FirstName,
                LastName = viewmodel.LastName,
                Zipcode = viewmodel.Zipcode,
                State = viewmodel.State
            };
        }
        public LocationViewModel ParseLocation(Location l)
        {
            return new LocationViewModel()
            {
                BranchName = l.BranchName,
                LocID = l.LocID
            };
        }
        public InventoryViewModel ParseInventory(Inventory i, Location l)
        {
            return new InventoryViewModel()
            {
                InventID = i.InventID,
                ProductName = i.Prod.Name,
                Stock = i.Stock,
                BanchName = l.BranchName
            };
        }
    }
}
