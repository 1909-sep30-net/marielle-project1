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

        // GET: Location Inventory
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

        // GET: Location Order History
        public ActionResult OrderHistory(int id)
        {
            List<Orders> lORdHist = _repository.GetLocationOrderHistory(id);
            Location l = _repository.GetLocationByID(id);
            LocationOrderHistoryViewModel local = _mapper.ParseLocationOrderHistory(lORdHist, l);
            Log.Information($"Viewed order history of { _repository.GetLocationByID(id).BranchName}");
            return View(local);
        }
    }
}