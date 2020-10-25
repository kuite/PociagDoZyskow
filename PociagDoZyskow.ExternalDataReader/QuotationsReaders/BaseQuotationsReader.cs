using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;
using PociagDoZyskow.ExternalDataReader.QuotationsReaders.Interfaces;

namespace PociagDoZyskow.ExternalDataReader.QuotationsReaders
{
    public abstract class BaseQuotationsReader : IExternalDataReader
    {
        public abstract string QuotationShortName { get; }
        public abstract string QuotationLink { get; }

        public abstract Task<IEnumerable<CompanyDataScan>> GetCompanyDailyDataScans(DateTime date);
    }
}
