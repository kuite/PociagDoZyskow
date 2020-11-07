using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PociagDoZyskow.ExternalDataHandler.ReportsWritter.Interfaces
{
    public interface IFinancialReportTimeWriter
    {
        Task<IEnumerable<FinancialReportTimeScan>> SaveReportDataScansToDatabase(IEnumerable<DTO.FinancialReportTimeScan> reportScans, IEnumerable<Company> relatedCompaniesEntities);
    }
}
