using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.QuotationsReader.Quotations.Interfaces
{
    public interface IQuotationsReader
    {
        Task<IEnumerable<CompanyDataScan>> GetExchangeScans(DateTime date);
    }
}
