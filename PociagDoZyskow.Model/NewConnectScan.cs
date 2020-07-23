using System;
using System.Collections.Generic;

namespace PociagDoZyskow.Model
{
    public class NewConnectDataScan
    {
        public DateTime ScanTime {get;set;}
        public List<CompanyDataScan> CompanyDataScans {get;set;}
    }
}
