using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using PociagDoZyskow.DataAccess.Mapping.ExternalDataReads;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public sealed class ExternalDataReadsContext : BaseContext
    {
        public DbSet<CompanyDataScan> CompanyDataScans { get; set; }

        public DbSet<FinancialReportTimeScan> FinancialReportTimeDataScans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyDataScanMap());
            modelBuilder.ApplyConfiguration(new FinancialReportTimeScanMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
