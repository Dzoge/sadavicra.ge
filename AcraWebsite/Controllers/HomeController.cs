using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AcraWebsite.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AcraWebsite.Models;

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
            return View(model);
        }
    }
}