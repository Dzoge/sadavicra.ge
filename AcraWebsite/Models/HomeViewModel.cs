using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcraWebsite.Models
{
    public class HomeViewModel
    {
        public string FilterVaccineId { get; }
        public string FilterRegionId { get; }

        public HomeViewModel(string vaccineFilter, string regionFilter)
        {
            if (IsFilterParameterValueValid(vaccineFilter))
                FilterVaccineId = vaccineFilter;

            if (IsFilterParameterValueValid(regionFilter))
                FilterRegionId = regionFilter;
        }

        public BookingDataOverview Overview { get; set; }

        private bool IsFilterParameterValueValid(string value)
        {
            if (value == null)
                return false;

            for (int i = 0; i < value.Length; i++)
                if (!Char.IsLetterOrDigit(value[i]) && value[i] != '-')
                    return false;

            return true;
        }
    }
}
