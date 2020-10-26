using System.Net;
using PociagDoZyskow.ExternalDataReader.ReportsReaders;
using Xunit;

namespace PociagDoZyskow.Tests.Integration.ExternalDataReader
{
    public class FinancialReportTimeReaderTests
    {
        [Fact]
        public async void GetPublishedFinancialReportTimeScans()
        {
            //Arrange
            var webClient = new WebClient();
            var financialReader = new FinancialReportTimeReader(webClient);

            //Act
            var result = await financialReader.GetPublishedFinancialReportTimeScans();

            //Assert
            Assert.NotEmpty(result);
        }
    }
}
