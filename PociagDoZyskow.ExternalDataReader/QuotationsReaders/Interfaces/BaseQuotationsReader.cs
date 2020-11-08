using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.Services.QuotationsReaders.Interfaces
{
    public abstract class BaseQuotationsReader : IQuotationsReader
    {
        public abstract string QuotationShortName { get; }
        public abstract string QuotationLink { get; }

        public abstract Task<IEnumerable<CompanyDataScan>> GetQuotationDataScansForDate(DateTime date);
    }
}
