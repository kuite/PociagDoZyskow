using Moq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using PociagDoZyskow.QuotationsReader.Quotations;
using Xunit;

namespace PociagDoZyskow.Tests.Integration.QuotationsReader
{
    public class GpwQuotationsReaderTests
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
            var result = await gpwReader.GetExchangeScans(date);

            //Assert
            Assert.Equal(436, result.Count());
        }
    }
}
