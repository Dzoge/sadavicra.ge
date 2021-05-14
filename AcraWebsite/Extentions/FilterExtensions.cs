using AcraWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcraWebsite
{
    public static class FilterExtensions
    {
        public static IEnumerable<Vaccine> MatchingFilter(this IEnumerable<Vaccine> vaccines, FilterViewModel filter)
        {
            return vaccines
                .Where(vaccine =>
                {
                    if (filter.VaccineId == null)
                        return true;

                    return vaccine.Id == filter.VaccineId;
                });
        }

        public static IEnumerable<Municipality> MatchingFilter(this IEnumerable<Municipality> municipalities, FilterViewModel filter)
        {
            return municipalities
                .Where(municipality =>
                {
                    if (filter.RegionId != null && municipality.RegionId != filter.RegionId)
                        return false;

                    if (filter.Date != null && !municipality.AvailableDates.Any(date => date == filter.DateValue))
                        return false;

                    return true;
                });
        }

        public static IEnumerable<VaccineLocation> MatchingFilter(this IEnumerable<VaccineLocation> locations, FilterViewModel filter)
        {
            return locations
                .Where(location =>
                {
                    if (filter.Date == null)
                        return true;

                    if (filter.Date != null && !location.AvailableDates.Any(date => date == filter.DateValue))
                        return false;

                    return true;
                });
        }
    }
}
