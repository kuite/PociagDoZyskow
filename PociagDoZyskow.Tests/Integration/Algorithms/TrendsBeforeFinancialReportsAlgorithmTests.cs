using Microsoft.Extensions.Configuration;
using Moq;
using PociagDoZyskow.Algorithms;
using PociagDoZyskow.Algorithms.DTO;
using PociagDoZyskow.DataAccess.Contexts;
using Xunit;

namespace PociagDoZyskow.Tests.Integration.Algorithms
{
    public class TrendsBeforeFinancialReportsReportTests
    {
        [Fact]
        public async void GetResultTest()
        {
            
            //Mock
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .SetupGet(x => x["ConnectionStrings:database"])
                .Returns("Data Source=(local);Initial Catalog=PociagDoZyskow;Integrated Security=True");
            var configuration = mockConfiguration.Object;
            var resultConfiguration = new Configuration
            {
                CompanyShortName = "JSW",
                DaysFromNowToInclude = 60
            };

            var externalDataReadsContext = new ExternalDataReadsContext(configuration);
            var databaseContext = new DatabaseContext(configuration);
            var algorithm = new TrendsBeforeFinancialReportsAlgorithm(externalDataReadsContext, databaseContext);

            //Act
            var results = await algorithm.CalculateResult(resultConfiguration);

            //Assert
        }
    }
}
