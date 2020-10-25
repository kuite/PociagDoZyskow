using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces
{
    public interface IFinancialReportTimeScanFactory
    {
        FinancialReportTimeScan GetFinancialReportTimeScanEntity(Exchange exchange, DTO.FinancialReportTimeScan report);
    }
}
