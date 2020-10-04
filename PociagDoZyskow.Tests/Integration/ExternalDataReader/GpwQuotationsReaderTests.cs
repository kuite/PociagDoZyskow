using Moq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using PociagDoZyskow.ExternalDataReader.Quotations;
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

            //Assert
            Assert.Equal(436, result.Count());
        }
    }
}
