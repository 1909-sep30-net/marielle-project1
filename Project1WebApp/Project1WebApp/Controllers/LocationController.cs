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
    public class LocationController : Controller
    {
        private readonly IRepository _repository;
        private VMapper _mapper = new VMapper();
        public LocationController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: Location
        public ActionResult Index()
        {
            var viewModel = new List<LocationViewModel>();
            foreach (Location l in _repository.GetLocations())
            {
                viewModel.Add(_mapper.ParseLocation(l));
            }
            return View(viewModel);
        }

        // GET: Location/Details/5
        public ActionResult GetInventory(int id)
        {
            List<InventoryViewModel> inv = new List<InventoryViewModel>();
            foreach (Inventory item in _repository.GetInventory(id))
            {
                inv.Add(_mapper.ParseInventory(item, _repository.GetLocationByID(id)));
            }
            return View(inv);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Location/Edit/5
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

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Location/Delete/5
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