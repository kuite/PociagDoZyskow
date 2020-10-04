using System;
using System.Collections.Generic;
using System.Text;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces
{
    public interface IFinancialReportTimeScanFactory
    {
        FinancialReportTimeScan GetFinancialReportTimeScanEntity(List<Exchange> exchanges, DTO.FinancialReportTimeScan report);
    }
}
