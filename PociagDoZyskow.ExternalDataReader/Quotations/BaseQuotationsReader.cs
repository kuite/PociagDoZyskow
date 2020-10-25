using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;
using PociagDoZyskow.ExternalDataReader.Quotations.Interfaces;

namespace PociagDoZyskow.ExternalDataReader.Quotations
{
    public abstract class BaseQuotationsReader : IExternalDataReader
    {
        public abstract string QuotationShortName { get; }
        public abstract string QuotationLink { get; }

        public abstract Task<IEnumerable<CompanyDataScan>> GetCompanyDailyDataScans(DateTime date);
    }
}
