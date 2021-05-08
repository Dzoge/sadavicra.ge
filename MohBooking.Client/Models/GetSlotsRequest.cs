using System;
using System.Text.Json.Serialization;

namespace MohBooking.Client
{
    public class GetSlotsRequest
    {
        [JsonPropertyName("branchID")]
        public string BranchId { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("regionID")]
        public string RegionId { get; set; }

        [JsonPropertyName("serviceID")]
        public string ServiceId { get; set; }
    }
}