using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces
{
    public interface IFinancialReportTimeScanFactory
    {
        FinancialReportTimeScan GetFinancialReportTimeScanEntity(List<Company> companies, DTO.FinancialReportTimeScan report);
    }
}
