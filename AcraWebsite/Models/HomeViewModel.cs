using System;
using System.Globalization;
using System.Linq;

namespace AcraWebsite.Models
{
    public class FilterViewModel
    {
        public string VaccineId { get; set; }
        public string RegionId { get; set; }
        public string Date { get; set; }
        public DateTime? DateValue { get; set; }
    }

    public class HomeViewModel
    {
        public CultureInfo CultureInfo { get; }
        public FilterViewModel Filter { get; }
        public LastUpdateDtSettings LastUpdateDtSettings { get; private set; }
        public BookingDataCache Cache { get; private set; }

        public HomeViewModel(string vaccineFilter, string regionFilter, string date)
        {
            CultureInfo = new CultureInfo("ka-ge");
            LastUpdateDtSettings = new LastUpdateDtSettings();
            Filter = GenerateFilter(vaccineFilter, regionFilter, date);
        }

        public void SetBookingData(BookingDataCache bookingDataCache)
        {
            if (bookingDataCache == null)
                return;

            Cache = bookingDataCache;
            GenerateLastUpdateStatus(Cache.LastUpdateDt);
        }

        public int GetAvailableCount(Vaccine vaccine, Municipality municipality)
        {
            return Cache.GetSlotData(vaccine.Id, municipality.Id, Filter.DateValue)
                .SelectMany(sd => sd.Dates)
                .SelectMany(d => d.Slots)
                .Count();
        }

        public int GetAvailableCount(Vaccine vaccine, Municipality municipality, VaccineLocation location)
        {
            return Cache.GetSlotData(vaccine.Id, municipality.Id, location.BranchId, Filter.DateValue)
                .SelectMany(sd => sd.Dates)
                .SelectMany(d => d.Slots)
                .Count();
        }

        private bool IsFilterParameterValueValid(string value)
        {
            if (value == null)
                return false;

            for (int i = 0; i < value.Length; i++)
                if (!Char.IsLetterOrDigit(value[i]) && value[i] != '-')
                    return false;

            return true;
        }

        private void GenerateLastUpdateStatus(DateTimeOffset lastUpdateDt)
        {
            LastUpdateDtSettings.LastUpdateDtDiffInMinutes = (int)((DateTimeOffset.Now - lastUpdateDt).TotalMinutes);

            if (LastUpdateDtSettings.LastUpdateDtDiffInMinutes >= 10)
            {
                LastUpdateDtSettings.FontSize = "14";
                LastUpdateDtSettings.ClassName = "danger";
                LastUpdateDtSettings.Emoji = "😒 ";
            }
            else if (LastUpdateDtSettings.LastUpdateDtDiffInMinutes >= 5)
            {
                LastUpdateDtSettings.FontSize = "14";
                LastUpdateDtSettings.ClassName = "warning";
                LastUpdateDtSettings.Emoji = "😬 ";
            }
        }

        private FilterViewModel GenerateFilter(string vaccineFilter, string regionFilter, string date)
        {
            var filter = new FilterViewModel();

            if (IsFilterParameterValueValid(vaccineFilter))
                filter.VaccineId = vaccineFilter;

            if (IsFilterParameterValueValid(regionFilter))
                filter.RegionId = regionFilter;

            if (IsFilterParameterValueValid(date))
            {
                filter.Date = date;
                filter.DateValue = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            return filter;
        }
    }
}
