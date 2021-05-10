using System;
using System.Globalization;

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

            LastUpdateDtSettings = new LastUpdateDtSettings();
        }

        public CultureInfo CultureInfo { get; set; }
        public LastUpdateDtSettings LastUpdateDtSettings { get; set; }
        public BookingDataCache Cache { get; set; }

        private bool IsFilterParameterValueValid(string value)
        {
            if (value == null)
                return false;

            for (int i = 0; i < value.Length; i++)
                if (!Char.IsLetterOrDigit(value[i]) && value[i] != '-')
                    return false;

            return true;
        }

        internal void GenerateLastUpdateStatus(DateTimeOffset? lastUpdateDt)
        {
            if (!lastUpdateDt.HasValue) return;
            var diff = (DateTimeOffset.Now - lastUpdateDt.Value).TotalMinutes;
            if (diff >= 10)
            {
                LastUpdateDtSettings.FontSize = "14";
                LastUpdateDtSettings.ClassName = "danger";
                LastUpdateDtSettings.Emoji = "🙈 ";
            }
            else if (diff >= 5)
            {
                LastUpdateDtSettings.FontSize = "14";
                LastUpdateDtSettings.ClassName = "warning";
                LastUpdateDtSettings.Emoji = "😬 ";
            }
        }
    }
    public class LastUpdateDtSettings
    {
        public string FontSize { get; set; } = "12";
        public string ClassName { get; set; } = "secondary";
        public string Emoji { get; set; }
    }
}
