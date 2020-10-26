using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities.Algorithms;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;

namespace PociagDoZyskow.DataAccess.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string Ticker { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public int ExchangeId { get; set; }

        public Exchange Exchange { get; set; }
    }
}
