using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces
{
    public interface IFinancialReportTimeScanFactory
    {
        FinancialReportTimeScan GetFinancialReportTimeScanEntity(List<Company> companies, DTO.FinancialReportTimeScan report);
    }
}
