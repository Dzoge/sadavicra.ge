using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MohBooking.Client
{
    public class Region
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        //[JsonPropertyName("olD_ID")]
        //public double? OlDID { get; set; }

        [JsonPropertyName("parentID")]
        public string ParentID { get; set; }

        [JsonPropertyName("parent")]
        public Region Parent { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("craCode")]
        public string CraCode { get; set; }

        [JsonPropertyName("geoName")]
        public string GeoName { get; set; }

        [JsonPropertyName("engName")]
        public string EngName { get; set; }

        [JsonPropertyName("newCode")]
        public string NewCode { get; set; }

        [JsonPropertyName("typeID")]
        public string TypeID { get; set; }

        [JsonPropertyName("areaType")]
        public AreaType AreaType { get; set; }

        //[JsonPropertyName("recordType")]
        //public int? RecordType { get; set; }

        //[JsonPropertyName("dateCreated")]
        //public DateTime? DateCreated { get; set; }

        //[JsonPropertyName("dateChanged")]
        //public object DateChanged { get; set; }

        //[JsonPropertyName("dateDeleted")]
        //public object DateDeleted { get; set; }
    }
    
     public class AreaType
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("geoName")]
        public string GeoName { get; set; }

        [JsonPropertyName("engName")]
        public string EngName { get; set; }

        //[JsonPropertyName("code")]
        //public int? Code { get; set; }

        //[JsonPropertyName("level")]
        //public int? Level { get; set; }

        //[JsonPropertyName("dateCreated")]
        //public DateTime? DateCreated { get; set; }

        //[JsonPropertyName("dateChanged")]
        //public object DateChanged { get; set; }

        //[JsonPropertyName("dateDeleted")]
        //public object DateDeleted { get; set; }
    }

    public class PhoneIndexType
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        //[JsonPropertyName("dateCreated")]
        //public DateTime? DateCreated { get; set; }

        //[JsonPropertyName("dateChanged")]
        //public object DateChanged { get; set; }

        //[JsonPropertyName("dateDeleted")]
        //public object DateDeleted { get; set; }
    }

    public class PhoneIndex
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        //[JsonPropertyName("value")]
        //public int? Value { get; set; }

        [JsonPropertyName("phoneIndexType")]
        public PhoneIndexType PhoneIndexType { get; set; }

        //[JsonPropertyName("dateCreated")]
        //public DateTime? DateCreated { get; set; }

        //[JsonPropertyName("dateChanged")]
        //public object DateChanged { get; set; }

        //[JsonPropertyName("dateDeleted")]
        //public object DateDeleted { get; set; }
    }

    //public class Parent
    //{
    //    [JsonPropertyName("id")]
    //    public string Id { get; set; }

    //    [JsonPropertyName("olD_ID")]
    //    public object OlDID { get; set; }

    //    [JsonPropertyName("parentID")]
    //    public object ParentId { get; set; }

    //    [JsonPropertyName("parent")]
    //    public object ParentElem { get; set; }

    //    [JsonPropertyName("code")]
    //    public object Code { get; set; }

    //    [JsonPropertyName("craCode")]
    //    public object CraCode { get; set; }

    //    [JsonPropertyName("geoName")]
    //    public string GeoName { get; set; }

    //    [JsonPropertyName("engName")]
    //    public object EngName { get; set; }

    //    [JsonPropertyName("newCode")]
    //    public string NewCode { get; set; }

    //    [JsonPropertyName("typeID")]
    //    public string TypeID { get; set; }

    //    [JsonPropertyName("areaType")]
    //    public AreaType AreaType { get; set; }

    //    [JsonPropertyName("phoneIndexes")]
    //    public List<PhoneIndex> PhoneIndexes { get; set; }

    //    [JsonPropertyName("recordType")]
    //    public int RecordType { get; set; }

    //    [JsonPropertyName("dateCreated")]
    //    public DateTime DateCreated { get; set; }

    //    [JsonPropertyName("dateChanged")]
    //    public object DateChanged { get; set; }

    //    [JsonPropertyName("dateDeleted")]
    //    public object DateDeleted { get; set; }
    //}
}