using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace PociagDoZyskow.HtmlReportsGenerator.DependencyInjection
{
    public static class ServiceResolver
    {
        public static ServiceProvider GetProvider()
        {
            var serviceProvider = new ServiceCollection();

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceProvider.AddSingleton<IConfiguration>(configuration);

            serviceProvider.AddScoped<IHtmlReportGenerator, HtmlReportGenerator>();
            //serviceProvider.AddLogging();
            //serviceProvider.AddScoped<WebClient>();
            //serviceProvider.AddScoped<DatabaseContext>();
            //serviceProvider.AddScoped<AlgorithmContext>();
            //serviceProvider.AddScoped<ExternalDataReadsContext>();
            //serviceProvider.AddScoped<ICompanyService, CompanyService>();
            //serviceProvider.AddScoped<GpwQuotationsReader>();
            //serviceProvider.AddScoped<GpwQuotationsWriter>();
            //serviceProvider.AddTransient<FinancialReportTimeProcessor>();
            //serviceProvider.AddTransient<GpwQuotationsDataProcessor>();

            var provider = serviceProvider.BuildServiceProvider();
            return provider;
        }
    }
}
