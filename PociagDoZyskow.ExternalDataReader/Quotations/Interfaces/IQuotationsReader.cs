using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.ExternalDataReader.Quotations.Interfaces
{
    public interface IExternalDataReader
    {
        Task<IEnumerable<CompanyDataScan>> GetCompanyDailyDataScans(DateTime date);
    }
}
