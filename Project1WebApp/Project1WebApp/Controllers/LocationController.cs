using Microsoft.AspNetCore.Mvc;
using Project1.BusinessLogic;
using Project1WebApp.Models;
using Serilog;
using System.Collections.Generic;

namespace Project1WebApp.Controllers
{
    /// <summary>
    /// Controller for Location operations
    /// </summary>
    public class LocationController : Controller
    {
        /// <summary>
        /// Repository to get data from db in business logic form
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Mapper that maps business logic objects to view models
        /// </summary>
        private VMapper _mapper = new VMapper();

        public LocationController(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Action that prints out all available locations
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var viewModel = new List<LocationViewModel>();
            foreach (Location l in _repository.GetLocations())
            {
                viewModel.Add(_mapper.ParseLocation(l));
            }
            return View(viewModel);
        }

        /// <summary>
        /// Action that takes location inventory of a certain location and prints it out
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetInventory(int id)
        {
            List<InventoryViewModel> inv = new List<InventoryViewModel>();
            foreach (Inventory item in _repository.GetInventory(id))
            {
                inv.Add(_mapper.ParseInventory(item, _repository.GetLocationByID(id)));
            }
            Log.Information($"Viewed { _repository.GetLocationByID(id).BranchName}'s inventory");
            return View(inv);
        }

        /// <summary>
        /// Action that takes location order history and prints it out
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OrderHistory(int id)
        {
            List<Orders> lORdHist = _repository.GetLocationOrderHistory(id);
            Location l = _repository.GetLocationByID(id);
            LocationOrderHistoryViewModel local = _mapper.ParseLocationOrderHistory(lORdHist, l);
            Log.Information($"Viewed order history of { _repository.GetLocationByID(id).BranchName}");
            if (lORdHist.Count < 1) Log.Information($"{ _repository.GetLocationByID(id).BranchName} has no order history because no orders have been placed in this location");
            return View(local);
        }
    }
}