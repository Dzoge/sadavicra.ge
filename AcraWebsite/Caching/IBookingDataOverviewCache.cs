using AcraWebsite.Models;

namespace AcraWebsite.Caching
{
    public interface IBookingDataOverviewCache
    {
        BookingDataOverview GetAllData();
        void InitiateDataReload();
    }
}
