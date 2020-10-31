using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using PociagDoZyskow.DataAccess.Mapping.ExternalDataReads;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public sealed class ExternalDataReadsContext : BaseContext
    {
        public DbSet<CompanyDataScan> CompanyDataScans { get; set; }

        public DbSet<FinancialReportTimeScan> FinancialReportTimeDataScans { get; set; }

        public ExternalDataReadsContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyDataScanMap());
            modelBuilder.ApplyConfiguration(new FinancialReportTimeScanMap());

            base.OnModelCreating(modelBuilder);
        }


    }
}
