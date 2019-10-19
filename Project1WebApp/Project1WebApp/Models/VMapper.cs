using Project1.BusinessLogic;
using System.Collections.Generic;

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
            return new LocationOrderHistoryViewModel()
            {
                BranchName = l.BranchName,
                Order = ParseOrderList(lORdHist)
            };
        }

        private List<LocationOrdersViewModel> ParseOrderList(List<Orders> lORdHist)
        {
            List<LocationOrdersViewModel> custOrder = new List<LocationOrdersViewModel>();
            foreach (Orders o in lORdHist)
            {
                custOrder.Add(new LocationOrdersViewModel()
                {
                    OrdDate = o.Date,
                    OrdID = o.OrdID,
                    CustomerOrder = ParseCustomerOrder(o.CustOrder),
                    Total = o.Total,
                    CustName = o.Cust.FirstName + " " + o.Cust.LastName
                });
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

        internal PlaceOrderViewModelV2 ParseMenu(List<Inventory> list, int custID, int locID)
        {
            return new PlaceOrderViewModelV2()
            {
                CustID = custID,
                LocID = locID,
                availInventory = ParseAvailInventoryV2(list)
            };
        }

        internal OrderDetailsViewModel ParseOrderDetails(Orders o)
        {
            return new OrderDetailsViewModel()
            {
                CustomerName = o.Cust.FirstName + " " + o.Cust.LastName,
                LocationName = o.Location.BranchName,
                custBought = ParseCustOrder(o.CustOrder)
            };
        }

        private List<AvailInvViewModel> ParseCustOrder(List<Inventory> custOrder)
        {
            List<AvailInvViewModel> inv = new List<AvailInvViewModel>();
            foreach (Inventory item in custOrder)
            {
                inv.Add(new AvailInvViewModel()
                {
                    InventID = item.InventID,
                    Stock = item.Stock
                });
            }
            return inv;
        }

        private List<AvailInvViewModel> ParseAvailInventoryV2(List<Inventory> list)
        {
            List<AvailInvViewModel> availInv = new List<AvailInvViewModel>();
            foreach (Inventory item in list)
            {
                availInv.Add(new AvailInvViewModel()
                {
                    ProductName = item.Prod.Name,
                    Price = item.Prod.Price,
                    Stock = item.Stock,
                    InventID = item.InventID
                });
            }
            return availInv;
        }

        internal List<Inventory> ParseInvID(List<int> custBought, List<int> quantity)
        {
            List<Inventory> inv = new List<Inventory>();
            int i = 0;
            foreach (int item in custBought)
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
    }
}