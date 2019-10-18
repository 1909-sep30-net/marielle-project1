using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.BusinessLogic;
using Project1WebApp.Models;

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

        // GET: Customer/Details/5
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
                return RedirectToAction(nameof(Found), viewModel);
            }
            catch
            {

                return View(viewModel);
            }
        }

        public ActionResult Found(SearchViewModel viewModel) => View(_repository.GetCustomers(viewModel.FirstName, viewModel.LastName));

        // GET: Customer/Create
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
                // TODO: Add insert logic here

                _repository.AddCustomer(_mapper.ParseCustomer(viewModel));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult GetGetOrderHistory(int id)
        {
            List<Orders> custOrder = _repository.GetCustomerOrderHistory(id);
            Customer c = _repository.GetCustomerById(id);
            ViewData["CustName"] = c.FirstName + " " + c.LastName;
            List<CustomerOrdersViewModel> custOrderView = _mapper.ParseCustOrderList(custOrder);
            return View(custOrderView);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}