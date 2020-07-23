using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.Stock;

namespace PociagDoZyskow.DataAccess.Mapping
{
    public class RecordMap : IEntityTypeConfiguration<DataScan>
    {
        public void Configure(EntityTypeBuilder<DataScan> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(left => left.Company)
                .WithMany(right => right.Records)
                .HasForeignKey(foreign => foreign.CompanyId);

            builder.ToTable("Record","stock");
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.LastTransactionTime).HasColumnName("LastTransactionTime");
            builder.Property(p => p.ReferencePrice).HasColumnName("ReferencePrice");
            builder.Property(p => p.OpenPrice).HasColumnName("OpenPrice");
            builder.Property(p => p.LowestPrice).HasColumnName("LowestPrice");
            builder.Property(p => p.HighestPrice).HasColumnName("HighestPrice");
            builder.Property(p => p.LastPrice).HasColumnName("LastPrice");
            builder.Property(p => p.LastTransactionVolume).HasColumnName("LastTransactionVolume");
            builder.Property(p => p.TotalTransactionVolume).HasColumnName("TotalTransactionVolume");
            builder.Property(p => p.TransactionsCount).HasColumnName("TransactionsCount");
        }
    }
}
