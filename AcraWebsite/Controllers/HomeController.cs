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
        private readonly IOpenSlotService _openSlotService;
        public HomeController(
            ILogger<HomeController> logger,
            IBookingDataCacheService bookingDataOverviewCache,
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> GetSlot(string branchId, string regionId, string serviceId)
        {
            var slots = await _openSlotService.GetOpenSlots(serviceId, regionId, branchId);
            return PartialView("~/Views/Shared/Partials/_OpenSlotPartial.cshtml", slots);
        }

    }
}