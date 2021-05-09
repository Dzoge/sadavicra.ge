using AcraWebsite.Caching;
using Microsoft.Extensions.Logging;

namespace AcraWebsite.Jobs
{
    public class RefreshDataJob
    {
        private readonly IBookingDataOverviewCache _bookingDataOverviewCache;
        private readonly ILogger<RefreshDataJob> _logger;

        public RefreshDataJob(
            IBookingDataOverviewCache bookingDataOverviewCache,
            ILogger<RefreshDataJob> logger
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
