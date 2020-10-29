using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.ExternalDataHandler.QuotationsReaders.Interfaces
{
    public interface IExternalDataHandler
    {
        Task<IEnumerable<CompanyDataScan>> GetCompanyDailyDataScans(DateTime date);
    }
}
