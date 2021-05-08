using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MohBooking.Client
{
    public class MohBookingClient : IMohBookingClient
    {
        private readonly HttpClient _httpClient;

        public MohBookingClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<ServiceType>> GetServicesAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "CommonData/GetServicesTypes");
            return ProcessRequestAsync<IEnumerable<ServiceType>>(requestMessage);
        }

        public Task<IEnumerable<Region>> GetRegionsAsync(string serviceId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"CommonData/GetRegions?serviceId={serviceId}");
            return ProcessRequestAsync<IEnumerable<Region>>(requestMessage);
        }

        public Task<IEnumerable<Region>> GetMunicipalitiesAsync(string serviceId, string regionId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"CommonData/GetMunicipalities/{regionId}?serviceId={serviceId}");
            return ProcessRequestAsync<IEnumerable<Region>>(requestMessage);
        }

        public Task<IEnumerable<MunicipalityBranch>> GetMunicipalityBranchesAsync(string serviceId,
            string municipalityId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"CommonData/GetMunicipalityBranches/{serviceId}/{municipalityId}");
            return ProcessRequestAsync<IEnumerable<MunicipalityBranch>>(requestMessage);
        }

        public Task<IEnumerable<SlotResponse>> GetSlotsAsync(string serviceId, string regionId, string branchId)
        {
            var request = new GetSlotsRequest()
            {
                ServiceId = serviceId,
                RegionId = regionId,
                BranchId = branchId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(31)
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Booking/GetSlots")
            {
                Content = JsonContent.Create(request)
            };
            return ProcessRequestAsync<IEnumerable<SlotResponse>>(requestMessage);
        }

        private async Task<TResult> ProcessRequestAsync<TResult>(HttpRequestMessage requestMessage)
        {
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadFromJsonAsync<TResult>();
        }
    }
}