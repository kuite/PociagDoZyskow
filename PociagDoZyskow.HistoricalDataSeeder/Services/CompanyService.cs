using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.HistoricalDataSeeder.Services.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DatabaseContext _databaseContext;

        public CompanyService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<DTO.Company> CreateCompaniesFromQuotationsScans(IEnumerable<DTO.CompanyDataScan> dailyQuotationReads)
        {
            var companies = new List<DTO.Company>();
            foreach (DTO.CompanyDataScan scan in dailyQuotationReads)
            {
                var company = new DTO.Company
                {
                    ShortName = scan.CompanyShortName
                };
                companies.Add(company);
            }

            return companies;
        }

        public async Task<IEnumerable<Company>> SaveCompaniesToDatabase(IEnumerable<DTO.Company> companiesDto, string exchangeShortName)
        {
            var companies = new List<Company>();
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
                    var newCompany = new Company
                    {
                        ShortName = companyDto.ShortName,
                        Exchange = exchange,
                        ExchangeId = exchange.Id
                    };
                    await _databaseContext.AddAsync(newCompany);
                    companies.Add(newCompany);
                }
            }


            return companies;
        }

        public Task<IEnumerable<Company>> UpdateMany(IEnumerable<Company> company)
        {
            throw new NotImplementedException();
        }
    }
}
