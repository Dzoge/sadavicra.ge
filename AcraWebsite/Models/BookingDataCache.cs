using System;
using System.Collections.Generic;
using System.Linq;

namespace AcraWebsite.Models
{
    public class BookingDataCache
    {
        private Dictionary<string, IEnumerable<OpenSlotModel>> _slotsData;

        public List<Vaccine> Vaccines { get; set; }
        public List<Municipality> Municipalities { get; set; }
        public DateTimeOffset LastUpdateDt { get; set; }

        // SlotsData public property is needed for fallback data serialization/deserialization
        public Dictionary<string, IEnumerable<OpenSlotModel>> SlotsData
        {
            get => _slotsData;
            set => _slotsData = value;
        }

        public BookingDataCache()
        {
            _slotsData = new Dictionary<string, IEnumerable<OpenSlotModel>>();
        }

        public void AddSlotData(string vaccineId, string municipalityId, string branchId, IEnumerable<OpenSlotModel> data)
        {
            var key = GetSlotDataKey(vaccineId, municipalityId, branchId);
            if (_slotsData.ContainsKey(key))
                _slotsData.Remove(key);
            _slotsData.Add(key, data);
        }

        public IEnumerable<OpenSlotModel> GetSlotData(string vaccineId, string municipalityId, DateTime? date = null)
        {
            var keyPrefix = GetSlotDataKey(vaccineId, municipalityId);
            var keys = _slotsData.Keys
                .Where(k => k.StartsWith(keyPrefix))
                .ToArray();

            List<OpenSlotModel> list = new List<OpenSlotModel>();
            foreach (var key in keys)
            {
                list.AddRange(GetSlotData(key, date));
            }

            return list;
        }


        public IEnumerable<OpenSlotModel> GetSlotData(string vaccineId, string municipalityId, string branchId, DateTime? date)
        {
            var key = GetSlotDataKey(vaccineId, municipalityId, branchId);
            return GetSlotData(key, date);
        }
        public IEnumerable<OpenSlotModel> GetSlotData(string key, DateTime? date)
        {
            var result = new List<OpenSlotModel>();
            if (!_slotsData.ContainsKey(key))
                return result;

            if (!date.HasValue)
                return _slotsData[key];

            foreach (var slotDatain in _slotsData[key])
            {
                var datesList = new List<ScheduleDate>();
                foreach (var slotDate in slotDatain.Dates)
                {
                    if (slotDate.Dt != date.Value)
                        continue;

                    datesList.Add(slotDate);
                }

                if (datesList.Count > 0)
                    result.Add(new OpenSlotModel()
                    {
                        Name = slotDatain.Name,
                        Dates = datesList
                    });
            }

            return result;
        }

        private string GetSlotDataKey(string vaccineId, string municipalityId, string branchId)
        {
            return $"{vaccineId}-{municipalityId}-{branchId}";
        }
        private string GetSlotDataKey(string vaccineId, string municipalityId)
        {
            return $"{vaccineId}-{municipalityId}";
        }
    }

    public class Vaccine
    {
        public string ServiceId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Municipality> Municipalities { get; set; }
    }

    public class Municipality
    {
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<VaccineLocation> Locations { get; set; }
        public List<DateTime> AvailableDates { get; set; }
    }

    public class VaccineLocation
    {
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public List<DateTime> AvailableDates { get; set; }
    }
}
