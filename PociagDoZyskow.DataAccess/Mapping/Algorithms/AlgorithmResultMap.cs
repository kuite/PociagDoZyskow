using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PociagDoZyskow.DataAccess.Entities.Algorithms;

namespace PociagDoZyskow.DataAccess.Mapping.Algorithms
{
    public class AlgorithmResultMap : IEntityTypeConfiguration<AlgorithmResult>
    {
        public void Configure(EntityTypeBuilder<AlgorithmResult> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(left => left.Company)
                .WithMany(right => right.AlgorithmResults)
                .HasForeignKey(foreign => foreign.CompanyId);

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId");
            builder.Property(p => p.AlgorithmName).HasColumnName("AlgorithmName");
            builder.Property(p => p.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(p => p.ResultDescription).HasColumnName("ResultDescription");
            builder.Property(p => p.IsBuy).HasColumnName("IsBuy");
        }
    }
}
