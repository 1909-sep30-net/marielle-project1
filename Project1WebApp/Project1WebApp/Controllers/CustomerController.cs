using Microsoft.AspNetCore.Mvc;
using Project1.BusinessLogic;
using Project1WebApp.Models;
using Serilog;
using System.Collections.Generic;

namespace Project1WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository _repository;
        private VMapper _mapper = new VMapper();

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search Detials
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchViewModel viewModel)
        {
            try
            {
                Log.Information($"Searching for customer {viewModel.FirstName} {viewModel.LastName}");
                return RedirectToAction(nameof(Found), viewModel);
            }
            catch
            {
                Log.Error("Something went wrong when searching for a customer");
                return View(viewModel);
            }
        }

        public ActionResult Found(SearchViewModel viewModel)
        {
            Log.Information($"{_repository.GetCustomers(viewModel.FirstName, viewModel.LastName).Count} customers found");
            return View(_repository.GetCustomers(viewModel.FirstName, viewModel.LastName));

        }       // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel viewModel)
        {
            try
            {
                _repository.AddCustomer(_mapper.ParseCustomer(viewModel));
                Log.Information("Customer Added!");
                return RedirectToAction(nameof(HomeController.Index));
            }
            catch
            {
                Log.Error("Something went wrong when adding a customer");
                return View(viewModel);
            }
        }

        // GET: Customer Order History
        public ActionResult GetGetOrderHistory(int id)
        {
            List<Orders> custOrder = _repository.GetCustomerOrderHistory(id);
            Customer c = _repository.GetCustomerById(id);
            ViewData["CustName"] = c.FirstName + " " + c.LastName;
            List<CustomerOrdersViewModel> custOrderView = _mapper.ParseCustOrderList(custOrder);
            Log.Information($"Viewed order history of {ViewData["CustName"]}");
            return View(custOrderView);
        }

        public ActionResult SetLocation(int id)
        {
            var viewModel = new List<LocationViewModel>();
            foreach (Location l in _repository.GetLocations())
            {
                viewModel.Add(_mapper.ParseLocation(l));
            }
            ViewData["Locations"] = viewModel;
            ViewData["CustID"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(int LocID, int CustID)
        {
            PlaceOrderViewModelV2 AvailInvent = _mapper.ParseMenu(_repository.GetAvailInventory(LocID), CustID, LocID);
            return View(AvailInvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrder(PlaceOrderViewModelV2 viewModel)
        {
            try
            {
                Orders o = new Orders()
                {
                    Cust = _repository.GetCustomerById(viewModel.CustID),
                    Location = _repository.GetLocationByID(viewModel.LocID),
                    CustOrder = _mapper.ParseInvID(viewModel.custBought, viewModel.Quantity)
                };
                _repository.AddOrder(o);
                Log.Information("Order Added");
                Log.Information($"Order made by customer {_repository.GetCustomerById(viewModel.CustID).FirstName} {_repository.GetCustomerById(viewModel.CustID).LastName} at {_repository.GetLocationByID(viewModel.LocID).BranchName}");
                return RedirectToAction(nameof(OrderDetails), viewModel);
            }
            catch
            {
                Log.Error("Something went wrong in placing order");
                return View(viewModel);
            }
        }

        public ActionResult OrderDetails(PlaceOrderViewModelV2 viewModel)
        {
            Orders o = new Orders()
            {
                Cust = _repository.GetCustomerById(viewModel.CustID),
                Location = _repository.GetLocationByID(viewModel.LocID),
                CustOrder = _mapper.ParseInvID(viewModel.custBought, viewModel.Quantity)
            };
            OrderDetailsViewModel ordDeets = _mapper.ParseOrderDetails(o);
            decimal total = 0;
            foreach (AvailInvViewModel item in ordDeets.custBought)
            {
                item.ProductName = _repository.GetProductNameById(item.InventID);
                item.Price = _repository.GetProductPriceById(item.InventID);
                total += item.Price * item.Stock;
            }
            ViewData["Total"] = total;
            return View(ordDeets);
        }
    }
}