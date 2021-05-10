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

        public void AddSlotData(string vaccineId, string regionId, string branchId, IEnumerable<OpenSlotModel> data)
        {
            var key = GetSlotDataKey(vaccineId, regionId, branchId);
            if (_slotsData.ContainsKey(key))
                _slotsData.Remove(key);
            _slotsData.Add(key, data);
        }

        public IEnumerable<OpenSlotModel> GetSlotData(string vaccineId, string regionId, string branchId)
        {
            var key = GetSlotDataKey(vaccineId, regionId, branchId);
            return _slotsData.ContainsKey(key)
                ? _slotsData[key]
                : new List<OpenSlotModel>();
        }

        private string GetSlotDataKey(string vaccineId, string regionId, string branchId)
        {
            return $"{vaccineId}-{regionId}-{branchId}";
        }
    }

    public class Vaccine
    {
        public string ServiceId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Municipality> Municipalities { get; set; }

        public int AvailableCount
            => Municipalities
               ?.SelectMany(m => m.Locations)
               .Sum(l => l.AvailableCount)
               ?? 0;
    }

    public class Municipality
    {
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<VaccineLocation> Locations { get; set; }

        public int AvailableCount
            => Locations
               ?.Sum(l => l.AvailableCount)
               ?? 0;
    }

    public class VaccineLocation
    {
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public int AvailableCount { get; set; }
        public string BranchAddress { get; set; }
    }
}
