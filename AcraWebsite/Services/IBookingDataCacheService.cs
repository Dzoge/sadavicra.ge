using AcraWebsite.Models;

namespace AcraWebsite.Services
{
    public interface IBookingDataCacheService
    {
        BookingDataCache GetAllData();
        void InitiateDataReload();
    }
}
