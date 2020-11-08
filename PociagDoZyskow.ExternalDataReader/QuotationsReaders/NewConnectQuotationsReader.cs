using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;
using PociagDoZyskow.Services.QuotationsReaders.Interfaces;

namespace PociagDoZyskow.Services.QuotationsReaders
{
    public class NewConnectServices : BaseQuotationsReader
    {
        public override string QuotationShortName => "NC";

        public override string QuotationLink => "https://newconnect.pl/notowania=";

        public override Task<IEnumerable<CompanyDataScan>> GetQuotationDataScansForDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
