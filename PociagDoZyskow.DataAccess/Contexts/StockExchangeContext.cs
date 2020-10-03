using System;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.Algorithms;
using PociagDoZyskow.DataAccess.Mapping;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public class StockExchangeContext : BaseContext
    {
        public virtual DbSet<Exchange> StockExchanges { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<CompanyDataScan> Records { get; set; }

        public virtual DbSet<AlgorithmResult> AlgorithmResults { get; set; }

        public virtual DbSet<FinancialReportTimeDataScan> FinancialReportTimeDataScans { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new RecordMap());

            modelBuilder.Entity<Exchange>().HasData(new Exchange { Id = 1, Name = "NewConnect", ShortName = "NC" });
            modelBuilder.Entity<Company>().HasData(new Company
                { Id = 1, ExchangeId = 1, Name = "Cookieland", ShortName = "CL" });
            modelBuilder.Entity<CompanyDataScan>().HasData(new CompanyDataScan
            {
                Id = 1,
                CompanyId = 1,
                HighestPrice = 1.0m,
                LastPrice = 1.0m,
                LowestPrice = 1.0m,
                OpenPrice = 1.0m,
                ReferencePrice = 1.0m,
                TotalTransactionVolumeStockCount = 1,
                TransactionsCount = 1
            });
        }
    }
}
