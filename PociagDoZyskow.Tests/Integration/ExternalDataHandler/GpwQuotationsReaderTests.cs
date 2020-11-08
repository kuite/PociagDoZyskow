using System;
using System.Linq;
using System.Net;
using PociagDoZyskow.Services.QuotationsReaders;
using Xunit;

namespace PociagDoZyskow.Tests.Integration.Services
{
    public class GpwServicesTests
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
            var result = await gpwReader.GetQuotationDataScansForDate(date);
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
