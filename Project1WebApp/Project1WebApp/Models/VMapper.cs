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

        public LocationOrderHistoryViewModel ParseLocationOrderHistory(List<Orders> lORdHist, Location l)
        {
            return new LocationOrderHistoryViewModel() {
                BranchName = l.BranchName,
                Order = ParseOrderList(lORdHist)
            };
        }

        private List<LocationOrdersViewModel> ParseOrderList(List<Orders> lORdHist)
        {
            List<LocationOrdersViewModel> custOrder = new List<LocationOrdersViewModel>();
            foreach (Orders o in lORdHist)
            {
                custOrder.Add(new LocationOrdersViewModel() {
                    OrdDate = o.Date,
                    OrdID = o.OrdID,
                    CustomerOrder = ParseCustomerOrder(o.CustOrder),
                    Total = o.Total,
                    CustName = o.Cust.FirstName + " " + o.Cust.LastName
                }) ;
            }
            return custOrder;
        }
        public List<CustomerOrdersViewModel> ParseCustOrderList(List<Orders> cOrdHist)
        {
            List<CustomerOrdersViewModel> custOrder = new List<CustomerOrdersViewModel>();
            foreach (Orders o in cOrdHist)
            {
                custOrder.Add(new CustomerOrdersViewModel()
                {
                    OrdDate = o.Date,
                    OrdID = o.OrdID,
                    CustomerOrder = ParseCustomerOrder(o.CustOrder),
                    Total = o.Total,
                    BranchName = o.Location.BranchName
                    
                });
            }
            return custOrder;
        }

        private List<CustOrderViewModel> ParseCustomerOrder(List<Inventory> custOrder)
        {
            List<CustOrderViewModel> custO = new List<CustOrderViewModel>();
            foreach (Inventory i in custOrder)
            {
                custO.Add(new CustOrderViewModel()
                {
                    ProductName = i.Prod.Name,
                    Stock = i.Stock
                });
            }
            return custO;
        }

        public List<PlaceOrderViewModel> ParseInventory(List<Inventory> list, int CustID, int LocID)
        {
            List<PlaceOrderViewModel> availInv = new List<PlaceOrderViewModel>();
            foreach (Inventory item in list)
            {
                availInv.Add(ParseAvailInventory(item, CustID, LocID));
            }
            return availInv;
        }

        internal List<Inventory> ParseInvID(List<int> invBought, List<int> quantity)
        {
            List<Inventory> inv = new List<Inventory>();
            int i = 0;
            foreach (int item in invBought)
            {
                inv.Add(new Inventory()
                {
                    InventID = item,
                    Stock = quantity[i]
                    
                });
                i++;
            }
            return inv;
        }

        private PlaceOrderViewModel ParseAvailInventory(Inventory item, int c, int l)
        {
            return new PlaceOrderViewModel()
            {
                Price = item.Prod.Price,
                ProductName = item.Prod.Name,
                Stock = item.Stock,
                InvId = item.InventID,
                CustID = c,
                LocID = l
            };
        }
    }
}
