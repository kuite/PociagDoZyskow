using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;

namespace PociagDoZyskow.Services.Interfaces
{
    public interface ICreateFinancialTimeReportService
    {
        Task<IEnumerable<FinancialReportTimeScan>> SaveReportDataScansToDatabase(
            IEnumerable<DTO.FinancialReportTimeScan> reportScans, IEnumerable<Company> relatedCompaniesEntities);
    }
}
