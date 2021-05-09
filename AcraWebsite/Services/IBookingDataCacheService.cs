using AcraWebsite.Models;

namespace AcraWebsite.Services
{
    public interface IBookingDataCacheService
    {
        BookingDataOverview GetAllData();
        void InitiateDataReload();
    }
}
