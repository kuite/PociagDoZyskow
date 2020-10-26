﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;
using PociagDoZyskow.ExternalDataReader.QuotationsReaders.Interfaces;

namespace PociagDoZyskow.ExternalDataReader.QuotationsReaders
{
    public class NewConnectExternalDataReader : BaseQuotationsReader
    {
        public override string QuotationShortName => "NC";

        public override string QuotationLink => "https://newconnect.pl/notowania=";

        public override Task<IEnumerable<CompanyDataScan>> GetCompanyDailyDataScans(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}