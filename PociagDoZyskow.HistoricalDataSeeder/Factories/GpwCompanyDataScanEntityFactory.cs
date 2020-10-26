﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PociagDoZyskow.DataAccess.Entities;
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
        

        //public FinancialReportTimeScan GetFinancialReportTimeScanEntity(List<Exchange> exchanges, DTO.FinancialReportTimeScan report)
        //{
        //    var reportEntity =
        //        _mapper.Map<DTO.FinancialReportTimeScan, FinancialReportTimeScan>(report);

        //    //TODO: Refactor to:  exchanges.FirstOrDefault(e => e.Companies.Any(r => r.Name == reportEntity.FullCompanyName));
        //    var exchange = exchanges.FirstOrDefault(e => e.Name == "NewConnect");
        //    reportEntity.Company = new Company
        //    {
        //        Ticker = report.CompanyShortName,
        //        Name = report.FullCompanyName,
        //        ShortName = report.ShortCompanyName,
        //        Exchange = exchange
        //    };
        //    return reportEntity;
        //}

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
                    company = new Company();
                    company.Exchange = exchange;
                    company.ShortName = scan.CompanyShortName;
                }
                //var exchange = exchanges.FirstOrDefault(e => e.Name == "NewConnect");
                companyDataScanEntity.Company = company;
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