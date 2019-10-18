using Project1.BusinessLogic;
using Project1.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.DataAccess
{
    public class DMapper : IMapper
    {
        private readonly Project0DBContext _context;
        public DMapper(Project0DBContext context)
        {
            _context = context;
        }
        public BusinessLogic.Customer ParseCustomer(Entities.Customer c)
        {
            return new BusinessLogic.Customer()
            {
                City = c.City,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Street = c.Street,
                State = (States)Enum.Parse(typeof(States), c.State, true),
                Zipcode = int.Parse(c.ZipCode),
                CustID = c.CustId

            };
        }

        public Entities.Customer ParseCustomer(BusinessLogic.Customer c)
        {
            return new Entities.Customer()
            {
                City = c.City,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Street = c.Street,
                State = c.State.ToString(),
                ZipCode = c.Zipcode.ToString()
            };
        }

        public BusinessLogic.Inventory ParseInventory(Entities.Inventory i)
        {
            return new BusinessLogic.Inventory()
            {
                Prod = ParseProduct(_context.Product.Single(p => p.ProductId == i.ProductId)),
                Stock = i.Stock,
                InventID = i.InventoryId
            };
        }
        public BusinessLogic.Inventory ParseInventory(Entities.CustOrder o)
        {
            return new BusinessLogic.Inventory()
            {
                Prod = ParseProduct(_context.Product.Single(p => p.ProductId == o.ProductId)),
                Stock = o.Quantity,
                InventID = o.CustOrderId 
            };
        }

        public Entities.Inventory ParseInventory(BusinessLogic.Inventory i)
        {
            throw new NotImplementedException();
        }

        public BusinessLogic.Location ParseLocation(Entities.Location l)
        {
            return new BusinessLogic.Location()
            {
                LocID = l.LocationId,
                BranchName = l.BranchName,
                City = l.City,
                Street = l.Street,
                State = (States)Enum.Parse(typeof(States), l.State, true),
                Zipcode = int.Parse(l.ZipCode)


            };
        }

        public Entities.Location ParseLocation(BusinessLogic.Location l)
        {
            throw new NotImplementedException();
        }

        public BusinessLogic.Orders ParseOrders(Entities.Orders o)
        {
            return new BusinessLogic.Orders()
            {
                Cust = ParseCustomer(_context.Customer.Single(c => c.CustId == o.CustId)),
                Location = ParseLocation(_context.Location.Single(l => l.LocationId == o.LocationId)),
                CustOrder = ParseCustOrder(_context.CustOrder.Where(c => c.OrderId == o.OrderId).ToList()),
                Date = o.OrderDate,
                Total = o.Total
            };
        }

        private List<BusinessLogic.Inventory> ParseCustOrder(List<CustOrder> list)
        {
            List<BusinessLogic.Inventory> inv = new List<BusinessLogic.Inventory>();
            foreach (CustOrder c in list)
            {
                inv.Add(ParseInventory(c));
            }
            return inv;
        }

        public Entities.Orders ParseOrders(BusinessLogic.Orders o)
        {
            return new Entities.Orders()
            {
                OrderDate = DateTime.Now,
                LocationId = o.Location.LocID,
                CustId = o.Cust.CustID,
                CustOrder = ParseCustOrder(o.CustOrder, o.OrdID),

            };
        }

        private ICollection<CustOrder> ParseCustOrder(List<BusinessLogic.Inventory> custOrder, int OrdID)
        {
            ICollection<CustOrder> dbcustOrder = null;
            foreach (BusinessLogic.Inventory item in custOrder)
            {
                dbcustOrder.Add(new CustOrder()
                {
                    OrderId = OrdID,
                    Quantity = item.Stock,
                    Product = _context.Product.Single(p => p.ProductId == _context.Inventory.Single(inv => inv.InventoryId == item.InventID).ProductId)
                }); ;
            }
            return dbcustOrder;
        }

        public BusinessLogic.Product ParseProduct(Entities.Product p)
        {
            return new BusinessLogic.Product()
            {
                Price = p.Price,
                Name = p.Name,
                ProdID = p.ProductId
            };
        }

        public Entities.Product ParseProduct(BusinessLogic.Product p)
        {
            throw new NotImplementedException();
        }
    }
}