﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.ExternalDataHandler.QuotationsReaders;
using PociagDoZyskow.ExternalDataHandler.QuotationsWriter;
using PociagDoZyskow.HistoricalDataSeeder.Factories;
using PociagDoZyskow.HistoricalDataSeeder.Processors;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;
using PociagDoZyskow.HistoricalDataSeeder.Services;
using PociagDoZyskow.HistoricalDataSeeder.Services.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.DependencyInjection
{
    public static class ServiceResolver
    {
        public static ServiceProvider GetProvider()
        {
            var serviceProvider = new ServiceCollection();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            serviceProvider.AddSingleton(mapper);

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceProvider.AddSingleton<IConfiguration>(configuration);

            serviceProvider.AddLogging();
            serviceProvider.AddScoped<WebClient>();
            serviceProvider.AddScoped<DatabaseContext>();
            serviceProvider.AddScoped<AlgorithmContext>();
            serviceProvider.AddScoped<ExternalDataReadsContext>();
            serviceProvider.AddScoped<ICompanyService, CompanyService>();
            serviceProvider.AddScoped<GpwQuotationsReader>();
            serviceProvider.AddScoped<GpwQuotationsWriter>();
            serviceProvider.AddTransient<FinancialReportTimeProcessor>();
            serviceProvider.AddTransient<GpwQuotationsDataProcessor>();

            var provider = serviceProvider.BuildServiceProvider();
            return provider;
        }
    }
}