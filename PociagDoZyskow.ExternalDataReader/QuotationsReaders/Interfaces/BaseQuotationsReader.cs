using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.ExternalDataReader.QuotationsReaders.Interfaces
{
    public abstract class BaseQuotationsReader : IExternalDataReader
    {
        public abstract string QuotationShortName { get; }
        public abstract string QuotationLink { get; }

        public abstract Task<IEnumerable<CompanyDataScan>> GetCompanyDailyDataScans(DateTime date);
    }
}
