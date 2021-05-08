using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MohBooking.Client
{
    public class ServiceType
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        //[JsonPropertyName("minAge")]
        //public int MinAge { get; set; }

        [JsonPropertyName("allowed")]
        public int? Allowed { get; set; }

        //[JsonPropertyName("published")]
        //public bool Published { get; set; }

        //[JsonPropertyName("sameProvider")]
        //public bool SameProvider { get; set; }

        //[JsonPropertyName("allowForeigner")]
        //public bool AllowForeigner { get; set; }

        //[JsonPropertyName("ignoreAgeWhenDoctor")]
        //public bool IgnoreAgeWhenDoctor { get; set; }

        [JsonPropertyName("minBookingDays")]
        public int? MinBookingDays { get; set; }

        [JsonPropertyName("maxBookingDays")]
        public int? MaxBookingDays { get; set; }

        //[JsonPropertyName("immunizationID")]
        //public string ImmunizationId { get; set; }
    }
}