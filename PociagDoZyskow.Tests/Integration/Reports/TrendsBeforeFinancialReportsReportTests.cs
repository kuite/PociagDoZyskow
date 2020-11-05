using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using PociagDoZyskow.Algorithms;
using PociagDoZyskow.Algorithms.DTO;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.EmailReports.Factories;
using PociagDoZyskow.EmailReports.Reports;
using Xunit;

namespace PociagDoZyskow.Tests.Integration.Reports
{
    public class TrendsBeforeFinancialReportsReportTests
    {
        [Fact]
        public async void GetFilledTemplateTest()
        {
            var templatePath = @"C:\Users\DELL\Desktop\template.html";
            //Mock
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .SetupGet(x => x["ConnectionStrings:database"])
                .Returns("Data Source=(local);Initial Catalog=PociagDoZyskow;Integrated Security=True");
            mockConfiguration
                .SetupGet(x => x["ConnectionStrings:templates"])
                .Returns(@"C:\projects\PociagDoZyskow\PociagDoZyskow.Reports\Templates");
            var configuration = mockConfiguration.Object;
            var resultConfiguration = new Configuration
            {
                CompanyShortName = "CCC",
                DaysFromNowToInclude = 60
            };

            var externalDataReadsContext = new ExternalDataReadsContext(configuration);
            var databaseContext = new DatabaseContext(configuration);
            var algorithm = new TrendsBeforeFinancialReportsAlgorithm(externalDataReadsContext, databaseContext);
            var templateInfoFactory = new TemplateInfoFactory(configuration);
            var report = new TrendsBeforeFinancialReportsReport(templateInfoFactory);

            //Act
            var algorithmResults = await algorithm.GetResult(resultConfiguration);
            var filledReport = report.GetFilledTemplate(algorithmResults);

            File.WriteAllText(templatePath, filledReport);
            //Assert
        }
    }
}
