using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MohBooking.Client
{
    public class MohBookingNewClient : IMohBookingNewClient
    {
        private static Random _random = new Random();
        private readonly HttpClient _httpClient;

        public MohBookingNewClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<ServiceType>> GetServicesAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "GetServicesTypes");
            return ProcessRequestAsync<IEnumerable<ServiceType>>(requestMessage);
        }

        public Task<IEnumerable<Region>> GetRegionsAsync(string serviceId)
        {
            //_httpClient.BaseAddress = new Uri("https://booking.moh.gov.ge/Hmis/Hmis.Queue.API/api/");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"GetAvailableSlots/{serviceId}");
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
            requestMessage.Headers.Add("securitynumber", GenerateSecurityNumber());
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadFromJsonAsync<TResult>();
        }

        private string GenerateSecurityNumber()
        {
            DateTime now = DateTime.Now;
            int dateValue = now.Year * now.Month * now.Day;
            double randomVal = Math.Floor(0.5305393530878462f * 1000000f);
            long finalValue = (long)(dateValue * randomVal);
            return finalValue.ToString();
        }
    }
}