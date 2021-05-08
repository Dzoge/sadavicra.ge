using System.Text.Json.Serialization;

namespace MohBooking.Client
{
    public class MunicipalityBranch
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("parentID")]
        public string ParentId { get; set; }

        [JsonPropertyName("tax")]
        public string Tax { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("regionID")]
        public string RegionId { get; set; }

        [JsonPropertyName("municipality")]
        public string Municipality { get; set; }

        [JsonPropertyName("municipalityID")]
        public string MunicipalityId { get; set; }

        [JsonPropertyName("settlement")]
        public string Settlement { get; set; }

        [JsonPropertyName("settlementID")]
        public string SettlementId { get; set; }

    }
}