using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories
{
    public class FinancialReportTimeScanEntityFactory : IFinancialReportTimeScanFactory
    {
        public IMapper _mapper;

        public FinancialReportTimeScanEntityFactory(IMapper iMapper)
        {
            _mapper = iMapper;
        }
        

        public FinancialReportTimeScan GetFinancialReportTimeScanEntity(List<Company> companies, DTO.FinancialReportTimeScan report)
        {
            var reportEntity =
                _mapper.Map<DTO.FinancialReportTimeScan, FinancialReportTimeScan>(report);

            //TODO: Refactor to:  exchanges.FirstOrDefault(e => e.Companies.Any(r => r.Name == reportEntity.FullCompanyName));
            //var exchange = exchanges.FirstOrDefault(e => e.Name == "NewConnect");
            var company = companies
                .FirstOrDefault(c => c.ShortName == report.ShortCompanyName);
            if (company == null)
            {
                //var x = 5;
                Console.WriteLine($"Not found company {report.FullCompanyName} for financialReportScan");
                //throw new Exception("Not found company for financialReportScan");
                return null;
            }
            else
            {
                //TODO: Update company  
                if (company.Ticker == null)
                {
                    company.Ticker = report.CompanyTicker;
                }
                if (company.Name == null)
                {
                    company.Name = report.FullCompanyName;
                }
            }

            reportEntity.CompanyId = company.Id;
            return reportEntity;
        }
    }
}
