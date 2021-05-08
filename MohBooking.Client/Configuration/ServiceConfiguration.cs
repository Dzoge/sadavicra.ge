﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace MohBooking.Client
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddMohBookingClientServices(this IServiceCollection services)
        {
            services.AddSingleton<IMohBookingClient, MohBookingClient>();

            services.AddHttpClient<IMohBookingClient, MohBookingClient>()
                .ConfigureHttpClient((serviceProvider, client) =>
                {
                    client.BaseAddress = new Uri("https://booking.moh.gov.ge/Hmis/Hmis.Queue.API/api/");
                    client.DefaultRequestHeaders.Add("referer", "https://booking.moh.gov.ge/Hmis/Hmis.Queue.Web/");
                    client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                    client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                    client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                    client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                    client.DefaultRequestHeaders.Add("securitynumber", "60949641360");
                    client.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.27 Safari/537.36");
                });

            return services;
        }
    }
}
