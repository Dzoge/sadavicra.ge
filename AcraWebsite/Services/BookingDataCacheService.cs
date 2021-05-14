﻿using AcraWebsite.Models;
using Microsoft.Extensions.Logging;
using MohBooking.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AcraWebsite.Services
{
    public class BookingDataCacheService : IBookingDataCacheService
    {
        public const int _defaultSleepIntervalMs = 100;

        private readonly IMohBookingClient _mohBookingClient;
        private readonly ILogger<BookingDataCacheService> _logger;
        private readonly object _dataLocker;

        private BookingDataCache _cachedData;
        private Thread _loadingThread;

        public BookingDataCacheService(
            IMohBookingClient mohBookingClient,
            ILogger<BookingDataCacheService> logger
        )
        {
            _mohBookingClient = mohBookingClient;
            _logger = logger;
            _dataLocker = new object();
            InitiateDataReload();
        }

        public BookingDataCache GetAllData()
        {
            lock (_dataLocker)
            {
                return _cachedData;
            }
        }

        public void InitiateDataReload()
        {
            lock (_dataLocker)
            {
                if (_loadingThread != null)
                    return;

                _loadingThread = new Thread(async () => await LoadingThreadWorker());
                _loadingThread.Start();
            }
        }

        private async Task LoadingThreadWorker()
        {
            BookingDataCache data;
            try
            {
                data = await LoadData();
                lock (_dataLocker)
                {
                    _cachedData = data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load data from MOH");

                lock (_dataLocker)
                {
                    if (_cachedData == null)
                        _cachedData = GetFallbackData();
                }
            }
            finally
            {
                _loadingThread = null;
            }
        }

        private async Task<BookingDataCache> LoadData()
        {
            int sleepInteval = 0;
            lock (_dataLocker)
            {
                if (_cachedData != null)
                    sleepInteval = _defaultSleepIntervalMs;
            }

            var model = new BookingDataCache();
            model.Vaccines = new List<Vaccine>();

            var vaccines = await _mohBookingClient.GetServicesAsync();
            Thread.Sleep(sleepInteval);

            foreach (var vaccine in vaccines)
            {
                var vaccineModel = new Vaccine()
                {
                    Id = vaccine.Key
                        .Trim(),
                    ServiceId = vaccine.Id,
                    Name = vaccine.Name
                        .Replace("კოვიდ 19 ვაქცინაცია", String.Empty)
                        .Replace("(", String.Empty)
                        .Replace(")", String.Empty)
                        .Trim(),
                    Description = GenerateVaccineDescription(vaccine.Id),
                    Municipalities = new List<Municipality>()
                };
                model.Vaccines.Add(vaccineModel);

                var regions = await _mohBookingClient.GetRegionsAsync(vaccine.Id);
                Thread.Sleep(sleepInteval);

                foreach (var region in regions)
                {
                    var municipalities = await _mohBookingClient.GetMunicipalitiesAsync(vaccine.Id, region.Id);
                    foreach (var municipality in municipalities)
                    {
                        var municipalityModel = new Municipality()
                        {
                            RegionId = region.Id,
                            RegionName = region.GeoName,
                            Id = municipality.Id,
                            Name = municipality.GeoName,
                            Locations = new List<VaccineLocation>(),
                            AvailableDates = new List<DateTime>()
                        };

                        var branches = await _mohBookingClient.GetMunicipalityBranchesAsync(vaccine.Id, municipality.Id);
                        Thread.Sleep(sleepInteval);

                        foreach (var branch in branches)
                        {
                            var slots = await _mohBookingClient.GetSlotsAsync(vaccine.Id, region.Id, branch.Id);
                            Thread.Sleep(sleepInteval);

                            var availableSlots = slots
                                .SelectMany(s => s.Schedules)
                                .SelectMany(s => s.Dates)
                                .SelectMany(d => d.Slots)
                                .Where(s => s.Taken != true && s.Reserved != true)
                                .ToList();

                            if (!availableSlots.Any())
                                continue;

                            var openSlots = MapSlotResponse(slots);
                            var distinctSlotDates = openSlots
                                .SelectMany(s => s.Dates)
                                .Where(d => d.Dt.HasValue)
                                .Select(d => d.Dt.Value.Date)
                                .Distinct()
                                .ToList();

                            model.AddSlotData(vaccine.Key, municipality.Id, branch.Id, openSlots);
                            var modelLocation = new VaccineLocation()
                            {
                                BranchId = branch.Id,
                                BranchName = branch.Name,
                                BranchAddress = branch.Address,
                                AvailableDates = distinctSlotDates
                            };
                            municipalityModel.Locations.Add(modelLocation);

                            municipalityModel.AvailableDates.AddRange(distinctSlotDates);
                        }

                        var slotsForMuniciplity = model.GetSlotData(vaccine.Key, municipality.Id);
                        if (slotsForMuniciplity.Count() > 0)
                        {
                            vaccineModel.Municipalities.Add(municipalityModel);
                            municipalityModel.AvailableDates = municipalityModel.AvailableDates
                                .Distinct()
                                .ToList();
                        }
                    }
                }
            }

            model.Municipalities = model.Vaccines
                .SelectMany(v => v.Municipalities)
                .GroupBy(m => m.RegionId)
                .Select(g => g.First())
                .ToList();

            model.LastUpdateDt = DateTimeOffset.Now;
            return model;
        }

        private string GenerateVaccineDescription(string vaccineId)
        {
            switch (vaccineId)
            {
                case "efc7f5d4-f4b1-4095-ad53-717389ea8258":
                    return
                        "21 აპრილიდან თქვენ შეგიძლიათ დაჯავშნოთ ადგილი ფაიზერის მე-2 დოზის მისაღებად, რისთვისაც უნდა გქონდეთ ჩატარებული 1 დოზით აცრა.\n" +
                        "აუცილებელია, რომ ჯავშანი გააკეთოთ იმავე კლინიკაში სადაც ჩაიატარეთ პირველი აცრა.\n" +
                        "აუცილებელია, რომ ჯავშანი გააკეთოთ 21-30 დღიან შუალედში პირველი აცრიდან და არ ჩაეწეროთ 20 დღიან ინტერვალის გასვლამდე.\n" +
                        "გთხოვთ შეინახეთ ჯავშნის კოდი, ვინაიდან ის დაგჭირდებათ უახლოეს მომავალაში კოვიდ პასპორტის მობილური აპლიკაციის გასააქტიურებლად\n";

                case "4bb6c283-3afb-436c-9974-6730cd2a18bd":
                    return "5 მაისიდან 20.00 სთ-დან იწყება ასტრაზენეკას მე-2 დოზაზე რეგისტრაცია. აცრის ჩატარება შესაძლებელი იქნება 10 მაისიდან. თქვენ შეგიძლიათ დარეგისტრირდეთ მე-2 დოზის მისაღებად ნებისმიერ დაწესებულებაში პირველი აცრიდან 4 - 12 კვირის ინტერვალში. ასაკობრივი შეზღუდვა არ ვრცელდება მათზე, ვინც პირველი დოზით აცრა ჩაიტარა ასტრაზენეკას ვაქცინით.\n" +
                           "დამატებითი ფანჯრები ეტაპობრივად გაიხსნება";

                case "d1eef49b-00b9-4760-9525-6100c168e642":
                    return "2021 წლის 12 მაისის 20.00 საათიდან იწყება რეგისტრაცია სინოფარმის ვაქცინის მეორე დოზით ვიზიტის დასაჯავშნად.\n" +
                           "ვიზიტის დაჯავშნა შეგიძლიათ 25 მაისიდან 10 ივნისამდე პერიოდზე\n" +
                           "12 მაისიდან შეჩერებული იქნება რეგისტრაცია ვაქცინის პირველ დოზაზე\n" +
                           "გთხოვთ, დაჯავშნოთ ვაქცინის მეორე დოზით აცრის ვიზიტი თქვენთვის სასურველ ნებისმიერ სამედიცინო დაწესებულებაში, პირველი დოზით აცრიდან 21-ე დღეს\n";
            }

            return null;
        }

        private BookingDataCache GetFallbackData()
        {
            var fileContent = File.ReadAllText("wwwroot/data/data-fallback.json");
            var fallbackData = JsonConvert.DeserializeObject<BookingDataCache>(fileContent);
            return fallbackData;
        }

        private List<OpenSlotModel> MapSlotResponse(IEnumerable<SlotResponse> slots)
        {
            return slots.Select(x =>
                 new OpenSlotModel()
                 {
                     Name = x.Name,
                     Dates = x.Schedules.FirstOrDefault().Dates
                         .Where(x => x.Slots.Any(s => s.Taken != true && s.Reserved != true))
                         .Select(y => new Models.ScheduleDate()
                         {
                             DateName = y.DateName,
                             Dt = y.Dt,
                             WeekName = y.WeekName,
                             Slots = y.Slots
                                .Where(s => s.Taken != true && s.Reserved != true)
                                .Select(z => new Models.Slot()
                                {
                                    Value = z.Value
                                })
                                .ToList()
                         })
                         .ToList()
                 }
             ).ToList();
        }
    }
}