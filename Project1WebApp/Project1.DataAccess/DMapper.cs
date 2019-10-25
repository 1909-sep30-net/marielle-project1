using Project1.BusinessLogic;
using Project1.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.DataAccess
{
    /// <summary>
    /// Class that maps the Business Logic Objects to the objects the DB return
    /// </summary>
    public class DMapper : IMapper
    {
        /// <summary>
        /// Context used to connect to the DB
        /// </summary>
        private readonly Project0DBContext _context;

        public DMapper(Project0DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that converts DB customers to business logic customers and vice versa
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method that converts DB inventory to Business Logic inventory and vice versa
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method that converts DB locations to Business Logic locations
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method takes in DB orders and converts them to Business Logic orders and vice versa
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
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

        public Entities.Orders ParseOrders(BusinessLogic.Orders o)
        {
            return new Entities.Orders()
            {
                OrderDate = DateTime.Now,
                LocationId = o.Location.LocID,
                CustId = o.Cust.CustID,
                CustOrder = ParseCustOrder(o.CustOrder),
                Total = CalculateTotal(o.CustOrder)
            };
        }

        /// <summary>
        /// Method that converts CustOrder from DB to Business Logic Inventory object types
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<BusinessLogic.Inventory> ParseCustOrder(List<CustOrder> list)
        {
            List<BusinessLogic.Inventory> inv = new List<BusinessLogic.Inventory>();
            foreach (CustOrder c in list)
            {
                inv.Add(ParseInventory(c));
            }
            return inv;
        }

        /// <summary>
        /// Method that converts Business Logic inventory object to DB CustOrder Entity
        /// </summary>
        /// <param name="custOrder"></param>
        /// <returns></returns>
        private ICollection<CustOrder> ParseCustOrder(List<BusinessLogic.Inventory> custOrder)
        {
            ICollection<CustOrder> dbcustOrder = new List<CustOrder>();
            foreach (BusinessLogic.Inventory item in custOrder)
            {
                dbcustOrder.Add(new Entities.CustOrder()
                {
                    Quantity = item.Stock,
                    ProductId = GetProductId(item.InventID)
                });
            }
            return dbcustOrder;
        }

        /// <summary>
        /// Method that Calculates Order Total
        /// </summary>
        /// <param name="custOrder"></param>
        /// <returns></returns>
        public decimal CalculateTotal(List<BusinessLogic.Inventory> custOrder)
        {
            decimal total = 0;
            foreach (BusinessLogic.Inventory item in custOrder)
            {
                total += _context.Product.Single(p => p.ProductId == GetProductId(item.InventID)).Price * item.Stock;
            }
            return total;
        }

        /// <summary>
        /// Method that gets a product id
        /// </summary>
        /// <param name="inventID"></param>
        /// <returns></returns>
        private int GetProductId(int inventID)
        {
            Entities.Inventory i = _context.Inventory.Single(inv => inventID == inv.InventoryId);
            return i.ProductId;
        }

        /// <summary>
        /// Method that converts DB Product to Business Logic Product
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public BusinessLogic.Product ParseProduct(Entities.Product p)
        {
            return new BusinessLogic.Product()
            {
                Price = p.Price,
                Name = p.Name,
                ProdID = p.ProductId
            };
        }
    }
}