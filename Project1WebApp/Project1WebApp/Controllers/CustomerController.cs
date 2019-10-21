using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project1.BusinessLogic;
using Project1WebApp.Models;
using Serilog;
using System;
using System.Collections.Generic;
namespace Project1WebApp.Controllers
{
    /// <summary>
    /// Controller for Customer and Order operations
    /// </summary>
    public class CustomerController : Controller
    {
        /// <summary>
        /// Repository to get data from database in business logic form
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Mapper that maps business logic data to view model data
        /// </summary>
        private VMapper _mapper = new VMapper();

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Primary functions of customer ui
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action that returns search UI
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// Action that takes in search parameters
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchViewModel viewModel)
        {
            try
            {
                Log.Information($"Searching for customer {viewModel.FirstName} {viewModel.LastName}");
                return RedirectToAction(nameof(Found), viewModel);
            }
            catch(Exception e)
            {
                Log.Error("Something went wrong when searching for a customer");
                Log.Error(e.Message);
                return View(viewModel);
            }
        }

        /// <summary>
        /// Action that returns found customers based on search parameters
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Found(SearchViewModel viewModel)
        {
            Log.Information($"{_repository.GetCustomers(viewModel.FirstName, viewModel.LastName).Count} customers found");
            return View(_repository.GetCustomers(viewModel.FirstName, viewModel.LastName));
        }

        /// <summary>
        /// Action that generates UI for customer addition
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Action that adds customer from user input data to db via repository
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
            catch (Exception e)
            {
                Log.Error("Something went wrong when adding a customer");
                Log.Error(e.Message);
                return View(viewModel);
            }
        }

        /// <summary>
        /// Action that gets order history of customer via repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetGetOrderHistory(int id)
        {
            List<Orders> custOrder = _repository.GetCustomerOrderHistory(id);
            Customer c = _repository.GetCustomerById(id);
            ViewData["CustName"] = c.FirstName + " " + c.LastName;
            List<CustomerOrdersViewModel> custOrderView = _mapper.ParseCustOrderList(custOrder);
            Log.Information($"Viewed order history of {ViewData["CustName"]}");
            if (custOrder.Count < 1) Log.Information($"Customer {ViewData["CustName"]} has no order history because they haven't placed any orders yet");
            return View(custOrderView);
        }

        /// <summary>
        /// Action that lets user set location of where an order will be placed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Action that takes location and customer to proceed with order
        /// </summary>
        /// <param name="LocID"></param>
        /// <param name="CustID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(int LocID, int CustID)
        {
            PlaceOrderViewModelV2 AvailInvent = _mapper.ParseMenu(_repository.GetAvailInventory(LocID), CustID, LocID);
            if (AvailInvent.availInventory.Count < 1) Log.Information($"{_repository.GetLocationByID(LocID).BranchName} has no inventory");
            return View(AvailInvent);
        }
        public ActionResult PlaceOrder()
        {
            var value = HttpContext.Session.GetString("CustID");
            int CustID = JsonConvert.DeserializeObject<int>(value);
            value = HttpContext.Session.GetString("LocID");
            int LocID = JsonConvert.DeserializeObject<int>(value);
            PlaceOrderViewModelV2 AvailInvent = _mapper.ParseMenu(_repository.GetAvailInventory(LocID), CustID, LocID);
            return View(AvailInvent);
        }
        /// <summary>
        /// Action that adds an order to db via repository
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrder(PlaceOrderViewModelV2 viewModel)
        {
            try
            {
                HttpContext.Session.SetString("CustID", JsonConvert.SerializeObject(viewModel.CustID));
                HttpContext.Session.SetString("LocID", JsonConvert.SerializeObject(viewModel.LocID));
                Orders o = new Orders()
                {
                    Cust = _repository.GetCustomerById(viewModel.CustID),
                    Location = _repository.GetLocationByID(viewModel.LocID),
                    CustOrder = _mapper.ParseInvID(viewModel.custBought)
                };
                _repository.AddOrder(o);
                Dictionary<int, int> custB = viewModel.custBought;
                HttpContext.Session.SetString("CustOrder", JsonConvert.SerializeObject(custB));

                Log.Information("Order Added");
                Log.Information($"Order made by customer {_repository.GetCustomerById(viewModel.CustID).FirstName} {_repository.GetCustomerById(viewModel.CustID).LastName} at {_repository.GetLocationByID(viewModel.LocID).BranchName}");
                PlaceOrderViewModelV2 pass = new PlaceOrderViewModelV2() { custBought = viewModel.custBought, CustID = viewModel.CustID, LocID = viewModel.LocID };
                return RedirectToAction(nameof(OrderDetails), pass);
            }
            catch(Exception e)
            {
                Log.Error("Something went wrong in placing order");
                Log.Error(e.Message);
                return RedirectToAction(nameof(PlaceOrder));
            }
        }

        /// <summary>
        /// Action that prints order details with data from adding an order
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult OrderDetails(PlaceOrderViewModelV2 viewModel)
        {
            var value = HttpContext.Session.GetString("CustOrder");
            Dictionary<int, int> custOrder = JsonConvert.DeserializeObject<Dictionary<int, int>>(value);
            Orders o = new Orders()
            {
                Cust = _repository.GetCustomerById(viewModel.CustID),
                Location = _repository.GetLocationByID(viewModel.LocID),
                CustOrder = _mapper.ParseInvID(custOrder)
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