using AcraWebsite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcraWebsite
{
    public class HealthCheckController : Controller
    {
        private readonly IBookingDataCacheService _bookingDataOverviewCache;

        public HealthCheckController(IBookingDataCacheService bookingDataOverviewCache)
        {
            _bookingDataOverviewCache = bookingDataOverviewCache;
        }
        public IActionResult Index()
        {
            var data = _bookingDataOverviewCache.GetAllData();
            if (data == null)
                return StatusCode(StatusCodes.Status204NoContent);

            var diff = (DateTimeOffset.Now - data.LastUpdateDt).TotalMinutes;

            if (diff > 10)
                return StatusCode(StatusCodes.Status503ServiceUnavailable);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
