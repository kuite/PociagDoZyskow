using PociagDoZyskow.Algorithms.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.Algorithms.DTO;
using PociagDoZyskow.DataAccess.Contexts;

namespace PociagDoZyskow.Algorithms
{
    public class TrendsBeforeFinancialReportsAlgorithm : BaseAlgorithm<TrendsBeforeFinancialReportsAlgorithmResult>
    {
        public override string AlgorithmName => nameof(TrendsBeforeFinancialReportsAlgorithm);

        private readonly ExternalDataReadsContext _externalDataReadsContext;

        private readonly DatabaseContext _databaseContext;

        public TrendsBeforeFinancialReportsAlgorithm(
            ExternalDataReadsContext externalDataReadsContext, 
            DatabaseContext databaseContext)
        {
            _externalDataReadsContext = externalDataReadsContext;
            _databaseContext = databaseContext;
        }

        public override async Task<TrendsBeforeFinancialReportsAlgorithmResult> GetResult(Configuration cfg)
        {
            var company = _databaseContext.Companies.FirstOrDefault(x => x.ShortName == cfg.CompanyShortName);
            if (company == null)
            {
                throw new Exception($"Not found {cfg.CompanyShortName} company.");
            }
            var companyReportTime = (await _externalDataReadsContext.FinancialReportTimeDataScans
                .Where(x => x.ShortCompanyName == cfg.CompanyShortName)
                .OrderBy(x => x.ReportDate)
                .ToListAsync())
                .Last();
            if (companyReportTime == null)
            {
                throw new Exception($"Not found financial report for {cfg.CompanyShortName} company within {cfg.DaysFromNowToInclude} days from today.");
            }
            var startDate = companyReportTime.ReportDate.AddDays(-cfg.DaysFromNowToInclude);
            var dailyQuotationScans = await _externalDataReadsContext.CompanyDataScans
                .Where(x => x.CompanyId == company.Id && x.ScanReferenceTime > startDate)
                .OrderBy(x => x.ScanReferenceTime)
                .ToListAsync();

            var stockMarketValues = dailyQuotationScans.Select(x => 
                new StockMarketValue
                {
                    ChangePrice = x.ChangePrice,
                    TotalTransactionValue = x.TotalTransactionVolumeStockCount,
                    ScanDateTime = x.ScanReferenceTime
                });

            var result = new TrendsBeforeFinancialReportsAlgorithmResult
            {
                CompanyShortName = company.ShortName,
                FinancialReportTime = companyReportTime.ReportDate,
                StockMarketValues = stockMarketValues
            };
            return result;
        }
    }
}
