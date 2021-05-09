using AcraWebsite.Caching;

namespace AcraWebsite.Jobs
{
    public class RefreshDataJob
    {
        private readonly IBookingDataOverviewCache _bookingDataOverviewCache;

        public RefreshDataJob(
            IBookingDataOverviewCache bookingDataOverviewCache
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
