using System.Collections.Generic;
using System.Threading.Tasks;

namespace MohBooking.Client
{
    public interface IMohBookingNewClient
    {
        Task<IEnumerable<ServiceType>> GetServicesAsync();
        Task<IEnumerable<Region>> GetRegionsAsync(string serviceId);
        Task<IEnumerable<Region>> GetMunicipalitiesAsync(string serviceId, string regionId);

        Task<IEnumerable<MunicipalityBranch>> GetMunicipalityBranchesAsync(string serviceId,
            string municipalityId);
        Task<IEnumerable<SlotResponse>> GetSlotsAsync(string serviceId, string regionId, string branchId);
    }
}