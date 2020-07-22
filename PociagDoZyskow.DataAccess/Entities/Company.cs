using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities.Stock;

namespace PociagDoZyskow.DataAccess.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public int ExchangeId { get; set; }

        public Exchange Exchange { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
