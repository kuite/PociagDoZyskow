using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.ExternalDataHandler.QuotationsReaders;
using PociagDoZyskow.HistoricalDataSeeder.Factories;
using PociagDoZyskow.HistoricalDataSeeder.Processors;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;

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

            serviceProvider.AddLogging();
            serviceProvider.AddScoped<WebClient>();
            serviceProvider.AddScoped<DatabaseContext>();
            serviceProvider.AddScoped<AlgorithmContext>();
            serviceProvider.AddScoped<ExternalDataReadsContext>();
            serviceProvider.AddScoped<GpwQuotationsReader>();
            serviceProvider.AddScoped<GpwCompanyDataScanEntityFactory>();
            serviceProvider.AddTransient<FinancialReportTimeReadsProcessor>();
            serviceProvider.AddTransient<GpwExternalDataReadsProcessor>();

            var provider = serviceProvider.BuildServiceProvider();
            return provider;
        }
    }
}
