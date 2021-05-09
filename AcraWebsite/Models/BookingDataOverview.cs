using System;
using System.Collections.Generic;
using System.Linq;

namespace AcraWebsite.Models
{
    public class BookingDataOverview
    {
        public List<Vaccine> Vaccines { get; set; }
        public List<Municipality> Municipalities { get; set; }
        public DateTimeOffset LastUpdateDt { get; set; }
        public Dictionary<string, IEnumerable<OpenSlotModel>> SlotData { get; set; }
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
