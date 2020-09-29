using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.QuotationsReader.Services.Interfaces
{
    public interface IQuotationsReader
    {
        Task<IEnumerable<CompanyDataScan>> GetExchangeScans(DateTime date);
    }
}
