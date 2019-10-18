﻿using Project1.BusinessLogic;
using Project1.DataAccess.Entities;
using System;
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
                Zipcode = int.Parse(c.ZipCode)

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
            throw new NotImplementedException();
        }

        public Entities.Orders ParseOrders(BusinessLogic.Orders o)
        {
            throw new NotImplementedException();
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