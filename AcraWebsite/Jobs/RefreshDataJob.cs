using AcraWebsite.Services;

namespace AcraWebsite.Jobs
{
    public class RefreshDataJob
    {
        private readonly IBookingDataCacheService _bookingDataOverviewCache;

        public RefreshDataJob(
            IBookingDataCacheService bookingDataOverviewCache
            )
        {
            _bookingDataOverviewCache = bookingDataOverviewCache;
        }

        public void Process()
        {
            _bookingDataOverviewCache.InitiateDataReload();
        }
    }
}
