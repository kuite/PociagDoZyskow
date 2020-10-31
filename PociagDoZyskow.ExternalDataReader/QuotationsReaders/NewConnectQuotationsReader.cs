using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;
using PociagDoZyskow.ExternalDataHandler.QuotationsReaders.Interfaces;

namespace PociagDoZyskow.ExternalDataHandler.QuotationsReaders
{
    public class NewConnectExternalDataHandler : BaseQuotationsReader
    {
        public override string QuotationShortName => "NC";

        public override string QuotationLink => "https://newconnect.pl/notowania=";

        public override Task<IEnumerable<CompanyDataScan>> GetQuotationDataScansForDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
