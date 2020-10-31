using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.HistoricalDataSeeder.Services.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<DTO.Company> CreateCompaniesFromQuotationsScans(IEnumerable<DTO.CompanyDataScan> dailyQuotationReads);

        Task<IEnumerable<Company>> SaveCompaniesToDatabase(IEnumerable<DTO.Company> companiesDto, string exchangeShortName);
    }
}
