using AcraWebsite.Models;
using AcraWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
        public IActionResult Index(string vaccine = null, string region = null)
        {
            var model = new HomeViewModel(vaccine, region);
            model.Cache = _bookingDataOverviewCache.GetAllData();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetSlot(string branchId, string regionId, string serviceId)
        {
            var data = _bookingDataOverviewCache.GetAllData();
            var slots = data.GetSlotData(serviceId, regionId, branchId);
            return PartialView("~/Views/Shared/Partials/_OpenSlotPartial.cshtml", slots);
        }

    }
}