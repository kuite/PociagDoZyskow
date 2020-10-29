using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.ExternalDataHandler.QuotationsReaders.Interfaces
{
    public abstract class BaseQuotationsReader : IExternalDataHandler
    {
        public abstract string QuotationShortName { get; }
        public abstract string QuotationLink { get; }

        public abstract Task<IEnumerable<CompanyDataScan>> GetCompanyDailyDataScans(DateTime date);
    }
}
