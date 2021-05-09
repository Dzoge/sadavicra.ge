using AcraWebsite.Models;
using MohBooking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcraWebsite.Service
{
    public interface IOpenSlotService
    {
        Task<IEnumerable<OpenSlotModel>> GetOpenSlots(string serviceId, string regionId, string branchId);
    }
    public class OpenSlotService : IOpenSlotService
    {
        private readonly IMohBookingClient _mohBookingClient;

        public OpenSlotService(
            IMohBookingClient mohBookingClient
        )
        {
            _mohBookingClient = mohBookingClient;
        }


        public async Task<IEnumerable<OpenSlotModel>> GetOpenSlots(string serviceId, string regionId, string branchId)
        {
            var slot =  await _mohBookingClient.GetSlotsAsync(serviceId, regionId, branchId);

            IEnumerable<OpenSlotModel> model = slot.Select(x =>
                new OpenSlotModel()
                {
                    Name = x.Name,
                    Dates = x.Schedules.FirstOrDefault().Dates
                    .Where(x=>x.Slots.Any(s=>s.Taken != true && s.Reserved != true))
                    .Select(y => new Models.ScheduleDate()
                    {
                        DateName = y.DateName,
                        Dt = y.Dt,
                        WeekName = y.WeekName,
                        Slots = y.Slots.Where(s => s.Taken != true && s.Reserved != true).Select(z => new Models.Slot()
                        {
                            Value = z.Value

                        })
                    })
                }
            ).AsEnumerable();

            return model;
        }
    }
}
