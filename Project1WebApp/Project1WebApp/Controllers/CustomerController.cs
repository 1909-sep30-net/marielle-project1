using Microsoft.AspNetCore.Mvc;
using Project1.BusinessLogic;
using Project1WebApp.Models;
using Serilog;
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
            catch
            {
                Log.Error("Something went wrong when searching for a customer");
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
            catch
            {
                Log.Error("Something went wrong when adding a customer");
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
        /// <summary>
        /// Action that prints order details with data from adding an order
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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