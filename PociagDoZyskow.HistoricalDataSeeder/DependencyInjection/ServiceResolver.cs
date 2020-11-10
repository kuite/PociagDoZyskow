using System;
using System.IO;
using System.Net;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.Services.QuotationsReaders;
using PociagDoZyskow.Services.ReportsReaders;
using PociagDoZyskow.HistoricalDataSeeder.Processors;
using PociagDoZyskow.Services.Implementations;
using PociagDoZyskow.Services.Interfaces;

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
            serviceProvider.AddScoped<ICreateCompanyService, CreateCompanyService>();
            serviceProvider.AddScoped<ICreateCompanyDataScanService, CreateCompanyDataScanService>();
            serviceProvider.AddScoped<GpwQuotationsReader>();
            serviceProvider.AddScoped<FinancialReportTimeReader>();
            serviceProvider.AddScoped<ICreateFinancialTimeReportService, CreateFinancialTimeReportService>();
            serviceProvider.AddTransient<FinancialReportTimeProcessor>();
            serviceProvider.AddTransient<GpwQuotationsDataProcessor>();

            var provider = serviceProvider.BuildServiceProvider();
            return provider;
        }
    }
}
