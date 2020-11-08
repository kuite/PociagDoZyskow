using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.Services.QuotationsReaders.Interfaces
{
    public interface IQuotationsReader
    {
        Task<IEnumerable<CompanyDataScan>> GetQuotationDataScansForDate(DateTime date);
    }
}
