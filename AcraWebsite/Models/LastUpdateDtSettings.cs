using System;

namespace AcraWebsite.Models
{
    public class LastUpdateDtSettings
    {
        public string FontSize { get; set; } = "12";
        public string ClassName { get; set; } = "secondary";
        public string Emoji { get; set; }
        public int LastUpdateDtDiffInMinutes { get; set; }

        public bool LastUpdateIsTooOld => LastUpdateDtDiffInMinutes >= 10;
        public bool LastUpdateIsBearable => LastUpdateDtDiffInMinutes >= 5;


        public string LastUpdateDtDisplayText
        {
            get
            {
                var differenceInHours = Math.Round(TimeSpan.FromMinutes(LastUpdateDtDiffInMinutes).TotalHours, 0);
                return LastUpdateDtDiffInMinutes >= 60 ? $"{differenceInHours} საათის" : $"{LastUpdateDtDiffInMinutes} წუთის";
            }
        }
    }
}
