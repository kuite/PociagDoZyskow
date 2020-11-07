using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DataAccess.Entities;
namespace PociagDoZyskow.HistoricalDataSeeder.Services.Interfaces
{
    public interface IFinancialReportService
    {
        IEnumerable<DTO.Company> CreateCompaniesFromReportScans(IEnumerable<DTO.FinancialReportTimeScan> reportScans);

        Task<IEnumerable<Company>> SaveCompaniesToDatabase(IEnumerable<DTO.Company> companiesDto, string exchangeShortName);
    }
}
