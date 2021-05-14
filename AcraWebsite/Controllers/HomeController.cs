using AcraWebsite.Models;
using AcraWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Globalization;

namespace AcraWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookingDataCacheService _bookingDataOverviewCache;
        public HomeController(
            ILogger<HomeController> logger,
            IBookingDataCacheService bookingDataOverviewCache
            )
        {
            _logger = logger;
            _bookingDataOverviewCache = bookingDataOverviewCache;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(string vaccine = null, string region = null, string date = null)
        {
            var model = new HomeViewModel(vaccine, region, date);
            model.SetBookingData(_bookingDataOverviewCache.GetAllData());
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetSlots(string branchId, string municipalityId, string serviceId, DateTime? date)
        {
            var data = _bookingDataOverviewCache.GetAllData();
            var slots = data.GetSlotData(serviceId, municipalityId, branchId, date);
            return PartialView("~/Views/Shared/Partials/_OpenSlotsPartial.cshtml", slots);
        }

    }
}