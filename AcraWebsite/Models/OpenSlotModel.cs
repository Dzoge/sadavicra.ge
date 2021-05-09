using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcraWebsite.Models
{
    public class OpenSlotModel
    {
        public string Name { get; set; }

        public IEnumerable<ScheduleDate> Dates { get; set; }
    }

    public class Slot
    {
        public string Value { get; set; }
    }

    public class ScheduleDate
    {
        public DateTime? Dt { get; set; }

        public string WeekName { get; set; }

        public string DateName { get; set; }

        public IEnumerable<Slot> Slots { get; set; }
    }
}
