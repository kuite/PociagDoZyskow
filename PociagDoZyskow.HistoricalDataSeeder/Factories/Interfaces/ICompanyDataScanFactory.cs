﻿using System;
using System.Collections.Generic;
using System.Text;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.HistoricalDataSeeder.Factories.Interfaces
{
    public interface ICompanyDataScanFactory
    {
        IEnumerable<CompanyDataScan> GetCompanyDataScanEntity(List<Company> companies, List<Exchange> exchanges, List<DTO.CompanyDataScan> companyScans);
    }
}