using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.DataAccess.Mapping
{
    public class FinancialReportTimeScanMap : IEntityTypeConfiguration<FinancialReportTimeScan>
    {
        public void Configure(EntityTypeBuilder<FinancialReportTimeScan> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(left => left.Company)
                .WithMany(right => right.FinancialReportTimeScans)
                .HasForeignKey(foreign => foreign.CompanyId);

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId");
            builder.Property(p => p.CompanyTicker).HasColumnName("CompanyTicker");
            builder.Property(p => p.FullCompanyName).HasColumnName("FullCompanyName");
            builder.Property(p => p.ReportDate).HasColumnName("ReportDate");
            builder.Property(p => p.ShortCompanyName).HasColumnName("ShortCompanyName");
            builder.Property(p => p.ReportType).HasColumnName("ReportType");
        }
    }
}
