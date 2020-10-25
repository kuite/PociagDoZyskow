using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.Algorithms;
using PociagDoZyskow.DataAccess.Mapping;
using PociagDoZyskow.DataAccess.Mapping.Algorithms;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public sealed class DatabaseContext : BaseContext
    {
        public DbSet<Exchange> Exchanges { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyDataScan> CompanyDataScans { get; set; }

        public DbSet<AlgorithmResult> AlgorithmResults { get; set; }

        public DbSet<FinancialReportTimeScan> FinancialReportTimeDataScans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlgorithmResultMap());
            modelBuilder.ApplyConfiguration(new CompanyDataScanMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new FinancialReportTimeScanMap());
            modelBuilder.ApplyConfiguration(new ExchangeMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
