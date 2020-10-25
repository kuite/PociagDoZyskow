using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories
{
    public class FinancialReportTimeScanEntityFactory : IFinancialReportTimeScanFactory
    {
        public IMapper _mapper { get; }

        public FinancialReportTimeScanEntityFactory(IMapper iMapper)
        {
            _mapper = iMapper;
        }
        

        public FinancialReportTimeScan GetFinancialReportTimeScanEntity(Company company, DTO.FinancialReportTimeScan report)
        {
            var reportEntity =
                _mapper.Map<DTO.FinancialReportTimeScan, FinancialReportTimeScan>(report);

            //TODO: Refactor to:  exchanges.FirstOrDefault(e => e.Companies.Any(r => r.Name == reportEntity.FullCompanyName));
            //var exchange = exchanges.FirstOrDefault(e => e.Name == "NewConnect");

            reportEntity.Company = company;
            return reportEntity;
        }
    }
}
