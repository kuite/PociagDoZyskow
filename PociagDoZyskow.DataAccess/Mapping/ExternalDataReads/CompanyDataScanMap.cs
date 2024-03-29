﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;

namespace PociagDoZyskow.DataAccess.Mapping.ExternalDataReads
{
    public class CompanyDataScanMap : IEntityTypeConfiguration<CompanyDataScan>
    {
        public void Configure(EntityTypeBuilder<CompanyDataScan> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.ReferencePrice).HasColumnName("ReferencePrice");
            builder.Property(p => p.OpenPrice).HasColumnName("OpenPrice");
            builder.Property(p => p.LowestPrice).HasColumnName("LowestPrice");
            builder.Property(p => p.HighestPrice).HasColumnName("HighestPrice");
            builder.Property(p => p.ChangePrice).HasColumnName("ChangePrice");
            builder.Property(p => p.LastPrice).HasColumnName("LastPrice");
            builder.Property(p => p.TotalTransactionVolumeStockCount).HasColumnName("TotalTransactionVolumeStockCount");
            builder.Property(p => p.TransactionsCount).HasColumnName("TransactionsCount");

            builder.ToTable("CompanyDataScans", "ExternalDataScans");
        }
    }
}
