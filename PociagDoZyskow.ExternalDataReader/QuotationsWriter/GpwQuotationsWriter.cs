using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using PociagDoZyskow.ExternalDataHandler.QuotationsWriter.Interfaces;

namespace PociagDoZyskow.ExternalDataHandler.QuotationsWriter
{
    public class GpwQuotationsWriter : IQuotationsWriter
    {
        private readonly ExternalDataReadsContext _externalDataReadsContext;

        private readonly DatabaseContext _databaseContext;

        private readonly IMapper _mapper;

        public GpwQuotationsWriter(
            ExternalDataReadsContext externalDataReadsContext, 
            DatabaseContext databaseContext, 
            IMapper mapper)
        {
            _externalDataReadsContext = externalDataReadsContext;
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDataScan>> SaveQuotationDataScansToDatabase(
            IEnumerable<DTO.CompanyDataScan> quotationScans, 
            IEnumerable<Company> relatedCompaniesEntities)
        {
            var newQuotationScanEntities = new List<CompanyDataScan>();
            if (quotationScans == null)
            {
                return newQuotationScanEntities;
            }

            var scanDates = quotationScans.Select(x => x.ScanReferenceTime).Distinct().ToList();
            var scansFromSavingDay = _externalDataReadsContext.CompanyDataScans
                .Where(x => scanDates.Contains(x.ScanReferenceTime));
            var companies = _databaseContext.Companies
                .Include(c => c.Exchange)
                .ToList();

            foreach (var quotationScan in quotationScans)
            {
                var quotationScanEntity =
                    _mapper.Map<DTO.CompanyDataScan, CompanyDataScan>(quotationScan);

                var company = companies.FirstOrDefault(c => c.ShortName == quotationScan.CompanyShortName);
                if (company == null)
                {
                    throw new Exception($"Not found company for quotation scan");
                }
                var exist = scansFromSavingDay.Any(x => x.CompanyId == company.Id);
                if (exist)
                {
                    continue;
                }
                quotationScanEntity.CompanyId = company.Id;
                newQuotationScanEntities.Add(quotationScanEntity);
            }

            await _externalDataReadsContext.CompanyDataScans.AddRangeAsync(newQuotationScanEntities);
            await _externalDataReadsContext.SaveChangesAsync();

            return newQuotationScanEntities;
        }
    }
}
