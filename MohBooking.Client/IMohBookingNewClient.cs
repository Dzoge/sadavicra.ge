using System.Collections.Generic;
using System.Threading.Tasks;

namespace MohBooking.Client
{
    public interface IMohBookingNewClient
    {
        Task<IEnumerable<ServiceType>> GetServicesAsync();
    }
}