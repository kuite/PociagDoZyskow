using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;
using PociagDoZyskow.Services.Interfaces.CRUD;
using Company = PociagDoZyskow.DataAccess.Entities.Company;

namespace PociagDoZyskow.Services.Interfaces
{
    public interface ICreateCompanyService : ICreateService
    {
        IEnumerable<DTO.Company> CreateCompaniesFromQuotationsScans(
            IEnumerable<DTO.CompanyDataScan> dailyQuotationReads);

        IEnumerable<DTO.Company> CreateCompaniesFromReportScans(IEnumerable<DTO.FinancialReportTimeScan> reportScans);

        Task<IEnumerable<Company>> SaveCompaniesToDatabase(IEnumerable<DTO.Company> companiesDto, string exchangeShortName);

        Task<IEnumerable<Company>> UpdateCompaniesFromReports(IEnumerable<FinancialReportTimeScan> financialReports);
    }
}
