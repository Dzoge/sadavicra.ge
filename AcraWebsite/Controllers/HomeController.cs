using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AcraWebsite.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AcraWebsite.Models;
using MohBooking.Client;
using AcraWebsite.Service;

namespace AcraWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookingDataOverviewCache _bookingDataOverviewCache;
        private readonly IOpenSlotService _openSlotService;
        public HomeController(
            ILogger<HomeController> logger,
            IBookingDataOverviewCache bookingDataOverviewCache,
            IOpenSlotService openSlotService
            )
        {
            _logger = logger;
            _openSlotService = openSlotService;
            _bookingDataOverviewCache = bookingDataOverviewCache;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(string vaccine = null, string region = null)
        {
            var model = new HomeViewModel(vaccine, region);
            model.Overview = _bookingDataOverviewCache.GetAllData();
            return View(model);
        }

        public async Task<IActionResult> GetSlot(string branchId, string regionId, string serviceId)
        {
            var slots = await _openSlotService.GetOpenSlots(serviceId, regionId, branchId);
            return PartialView("~/Views/Shared/Partials/_OpenSlotPartial.cshtml", slots);
        }

    }
}