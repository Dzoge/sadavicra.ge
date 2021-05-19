using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace MohBooking.Client
{
    public class SlotResponse
    {
        [JsonPropertyName("roomID")]
        public string RoomId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("onlyQueue")]
        public bool OnlyQueue { get; set; }

        [JsonPropertyName("schedules")]
        public List<Schedule> Schedules { get; set; }
    }

    public class Slot
    {
        [JsonPropertyName("organizationID")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("branchID")]
        public string BranchId { get; set; }

        //[JsonPropertyName("personID")]
        //public object PersonId { get; set; }

        [JsonPropertyName("scheduleDate")]
        public DateTime? ScheduleDate { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("roomID")]
        public string RoomID { get; set; }

        [JsonPropertyName("taken")]
        public bool? Taken { get; set; }

        [JsonPropertyName("reserved")]
        public bool? Reserved { get; set; }

        //[JsonPropertyName("scheduleDateTicks")]
        //public long? ScheduleDateTicks { get; set; }

        [JsonPropertyName("scheduleDateName")]
        public string ScheduleDateName { get; set; }
    }

    public class ScheduleDate
    {
        [JsonPropertyName("organizationID")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("branchID")]
        public string BranchId { get; set; }

        //[JsonPropertyName("personID")]
        //public object PersonId { get; set; }

        [JsonPropertyName("roomID")]
        public string RoomId { get; set; }

        //[JsonPropertyName("dateTicks")]
        //public long? DateTicks { get; set; }



        [JsonPropertyName("weekName")]
        public string WeekName { get; set; }

        [JsonPropertyName("dateName")]
        public string DateName { get; set; }
        //[JsonPropertyName("date")]
        public DateTime? Dt
        {
            get
            {
                DateTime convertedDt;
                return DateTime.TryParseExact(DateName, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out convertedDt)
                     ? convertedDt
                     : (DateTime?)null;
            }
        }

        [JsonPropertyName("slots")]
        public List<Slot> Slots { get; set; }
    }

    public class Schedule
    {
        [JsonPropertyName("roomID")]
        public string RoomId { get; set; }

        [JsonPropertyName("organizationID")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("branchID")]
        public string BranchId { get; set; }

        //[JsonPropertyName("personID")]
        //public object PersonId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        //[JsonPropertyName("title")]
        //public string Title { get; set; }

        [JsonPropertyName("dates")]
        public List<ScheduleDate> Dates { get; set; }
    }
}