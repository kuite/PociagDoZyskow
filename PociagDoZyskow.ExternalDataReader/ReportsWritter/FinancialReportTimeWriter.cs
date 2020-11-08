using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using PociagDoZyskow.Services.ReportsWritter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PociagDoZyskow.Services.ReportsWritter
{
    public class FinancialReportTimeWriter : IFinancialReportTimeWriter
    {
        private readonly ExternalDataReadsContext _externalDataReadsContext;
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public FinancialReportTimeWriter(
            ExternalDataReadsContext externalDataReadsContext, 
            DatabaseContext databaseContext,
            IMapper mapper)
        {
            _externalDataReadsContext = externalDataReadsContext;
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FinancialReportTimeScan>> SaveReportDataScansToDatabase(IEnumerable<DTO.FinancialReportTimeScan> reportScans, IEnumerable<Company> relatedCompaniesEntities)
        {
            var savedReportScans = new List<FinancialReportTimeScan>();
            var companies = await _databaseContext.Companies.Include(c => c.Exchange).ToListAsync();
            foreach (DTO.FinancialReportTimeScan financialReportTimeDataScan in reportScans)
            {
                var reportEntity = GetFinancialReportTimeScanEntity(companies, financialReportTimeDataScan);
                if (reportEntity != null)
                {
                    savedReportScans.Add(reportEntity);
                }
            }

            savedReportScans = RemoveDuplications(_externalDataReadsContext, _databaseContext, savedReportScans).ToList();
            await _externalDataReadsContext.FinancialReportTimeDataScans.AddRangeAsync(savedReportScans);
            await _externalDataReadsContext.SaveChangesAsync();
            return savedReportScans;
        }

        private IEnumerable<FinancialReportTimeScan> RemoveDuplications(ExternalDataReadsContext externalDataReadsContext, DatabaseContext context,
            List<FinancialReportTimeScan> newScans)
        {
            //TODO: will be in FinancialReportWriter
            var freshFinancialReportTimeScanEntities = new List<FinancialReportTimeScan>();
            var existedFinancialReportScans = externalDataReadsContext.FinancialReportTimeDataScans.ToList();
            var companies = context.Companies.ToList();

            foreach (FinancialReportTimeScan scan in newScans)
            {
                if (existedFinancialReportScans.Any(r =>
                    r.ShortCompanyName == scan.ShortCompanyName &&
                    r.ReportDate == scan.ReportDate))
                {
                    //not found company for this report
                    continue;
                }
                var relatedCompany = companies.FirstOrDefault(c => c.ShortName == scan.ShortCompanyName);
                if (relatedCompany == null)
                {
                    Console.WriteLine($"Not found company {scan.ShortCompanyName} for financial scan.");
                    continue;
                }
                scan.CompanyId = relatedCompany.Id;
                freshFinancialReportTimeScanEntities.Add(scan);
            }

            return freshFinancialReportTimeScanEntities;
        }

        private FinancialReportTimeScan GetFinancialReportTimeScanEntity(List<Company> companies, DTO.FinancialReportTimeScan report)
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

            reportEntity.CompanyId = company.Id;
            return reportEntity;
        }
    }
}
