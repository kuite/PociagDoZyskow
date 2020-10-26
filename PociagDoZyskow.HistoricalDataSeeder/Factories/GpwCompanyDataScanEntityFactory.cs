using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories
{
    public class GpwCompanyDataScanEntityFactory : ICompanyDataScanFactory
    {
        public IMapper _mapper { get; }

        public GpwCompanyDataScanEntityFactory(IMapper iMapper)
        {
            _mapper = iMapper;
        }
        

        public IEnumerable<CompanyDataScan> GetCompanyDataScanEntity(List<Company> companies, List<Exchange> exchanges, List<DTO.CompanyDataScan> companyScans)
        {
            var companyEntites = new List<CompanyDataScan>();
            foreach (DTO.CompanyDataScan scan in companyScans)
            {
                var companyDataScanEntity =
                    _mapper.Map<DTO.CompanyDataScan, CompanyDataScan>(scan);

                //TODO: Refactor to:  exchanges.FirstOrDefault(e => e.Companies.Any(r => r.Name == companyEntity.FullCompanyName));
                var company = companies.FirstOrDefault(c => c.ShortName == scan.CompanyShortName);
                var exchange = exchanges.FirstOrDefault(e => e.ShortName == "GPW");
                if (exchange == null)
                {
                    throw new Exception("Not found GPW exchange.");
                }
                if (company == null)
                {
                    //TODO: Save company  
                    company = new Company();
                    company.Exchange = exchange;
                    company.ShortName = scan.CompanyShortName;
                }
                //var exchange = exchanges.FirstOrDefault(e => e.Name == "NewConnect");
                companyDataScanEntity.CompanyId = company.Id;
                companyEntites.Add(companyDataScanEntity);
            }

            return companyEntites;
            //reportEntity.Company = new Company
            //{
            //    Ticker = report.CompanyShortName,
            //    Name = report.FullCompanyName,
            //    ShortName = report.ShortCompanyName,
            //    Exchange = exchange
            //};
            //return reportEntity;
        }
    }
}
