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

        private async Task<TResult> ProcessRequestAsync<TResult>(HttpRequestMessage requestMessage)
        {
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadFromJsonAsync<TResult>();
        }
    }
}