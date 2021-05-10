using AcraWebsite.Caching;
using AcraWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace AcraWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookingDataOverviewCache _bookingDataOverviewCache;

        public HomeController(
            ILogger<HomeController> logger,
            IBookingDataOverviewCache bookingDataOverviewCache
            )
        {
            _logger = logger;
            _bookingDataOverviewCache = bookingDataOverviewCache;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(string vaccine = null, string region = null)
        {
            var model = new HomeViewModel(vaccine, region);
            model.Overview = _bookingDataOverviewCache.GetAllData();
            model.CultureInfo = new CultureInfo("ka-ge");
            model.GenerateLastUpdateStatus(model.Overview?.LastUpdateDt);
            return View(model);
        }
    }
}