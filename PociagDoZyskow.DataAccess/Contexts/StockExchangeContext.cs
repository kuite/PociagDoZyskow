using System;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.Stock;
using PociagDoZyskow.DataAccess.Mapping;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public class StockExchangeContext : BaseContext
    {
        public virtual DbSet<Exchange> StockExchanges { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<DataScan> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new RecordMap());

            modelBuilder.Entity<Exchange>().HasData(new Exchange { Id = 1, Name = "NewConnect", ShortName = "NC" });
            modelBuilder.Entity<Company>().HasData(new Company
                { Id = 1, ExchangeId = 1, Name = "Cookieland", ShortName = "CL" });
            modelBuilder.Entity<DataScan>().HasData(new DataScan
            {
                Id = 1,
                CompanyId = 1,
                HighestPrice = 1.0m,
                LastPrice = 1.0m,
                LastTransactionTime = DateTime.UtcNow,
                LastTransactionVolume = 1,
                LowestPrice = 1.0m,
                OpenPrice = 1.0m,
                ReferencePrice = 1.0m,
                TotalTransactionVolume = 1,
                TransactionsCount = 1
            });
        }
    }
}
