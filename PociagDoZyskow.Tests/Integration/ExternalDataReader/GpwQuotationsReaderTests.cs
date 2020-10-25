using System;
using System.Linq;
using System.Net;
using PociagDoZyskow.ExternalDataReader.QuotationsReaders;
using Xunit;

namespace PociagDoZyskow.Tests.Integration.ExternalDataReader
{
    public class GpwExternalDataReaderTests
    {
        
        [Theory]
        [InlineData("25-09-2020")]
        public async void GetExchangeScans(string dateString)
        {
            //Arrange
            var date = DateTime.Parse(dateString);
            var webClient = new WebClient();
            var gpwReader = new GpwQuotationsReader(webClient);

            //Act
            var result = await gpwReader.GetCompanyDailyDataScans(date);
            var companyDataScans = result.ToList();
            var selectedScan = companyDataScans.FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(436, companyDataScans.Count);
            Assert.NotNull(selectedScan);
            Assert.NotNull(selectedScan.CompanyShortName);
        }
    }
}
