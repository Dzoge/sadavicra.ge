using AcraWebsite.Services;
using Microsoft.Extensions.Logging;

namespace AcraWebsite.Jobs
{
    public class RefreshDataJob
    {
        private readonly ILogger<RefreshDataJob> _logger;
        private readonly IBookingDataCacheService _bookingDataOverviewCache;

        public RefreshDataJob(
            ILogger<RefreshDataJob> logger,
            IBookingDataCacheService bookingDataOverviewCache
            )
        {
            _bookingDataOverviewCache = bookingDataOverviewCache;
            _logger = logger;
        }

        public void Process()
        {
            _bookingDataOverviewCache.InitiateDataReload();
            _logger.LogInformation("Data reload initiated");
        }
    }
}
