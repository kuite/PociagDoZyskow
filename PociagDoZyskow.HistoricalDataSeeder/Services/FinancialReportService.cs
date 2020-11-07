using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.HistoricalDataSeeder.Services.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly DatabaseContext _databaseContext;

        public FinancialReportService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<DTO.Company> CreateCompaniesFromReportScans(IEnumerable<DTO.FinancialReportTimeScan> reportScans)
        {
            var companies = new List<DTO.Company>();
            foreach (DTO.FinancialReportTimeScan scan in reportScans)
            {
                var company = new DTO.Company
                {
                    ShortName = scan.ShortCompanyName
                };
                companies.Add(company);
            }

            return companies;
        }

        public async Task<IEnumerable<DataAccess.Entities.Company>> SaveCompaniesToDatabase(IEnumerable<DTO.Company> companiesDto, string exchangeShortName)
        {
            var companies = new List<DataAccess.Entities.Company>();
            var existingCompanies = await _databaseContext.Companies.Include(x => x.Exchange)
                .ToListAsync();
            var exchange = _databaseContext.Exchanges.FirstOrDefault(e => e.ShortName == exchangeShortName);
            if (exchange == null)
            {
                throw new Exception($"Not found {exchangeShortName} exchange");
            }
            foreach (var companyDto in companiesDto)
            {
                var existingCompany = existingCompanies.FirstOrDefault(c => c.ShortName == companyDto.ShortName);
                if (existingCompany != null)
                {
                    companies.Add(existingCompany);
                }
                else
                {
                    var newCompany = new DataAccess.Entities.Company
                    {
                        ShortName = companyDto.ShortName,
                        Exchange = exchange,
                        ExchangeId = exchange.Id
                    };
                    await _databaseContext.AddAsync(newCompany);
                    await _databaseContext.SaveChangesAsync();
                    companies.Add(newCompany);
                }
            }


            return companies;
        }
    }
}
