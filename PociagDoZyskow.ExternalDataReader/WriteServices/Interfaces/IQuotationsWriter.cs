using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;

namespace PociagDoZyskow.Services.QuotationsWriter.Interfaces
{
    public interface IQuotationsWriter
    {
        Task<IEnumerable<CompanyDataScan>> SaveQuotationDataScansToDatabase(IEnumerable<DTO.CompanyDataScan> quotationScans, IEnumerable<Company> relatedCompaniesEntities);
    }
}
