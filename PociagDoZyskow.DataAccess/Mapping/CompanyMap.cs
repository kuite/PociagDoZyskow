using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.DataAccess.Mapping
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(left => left.Exchange)
                .WithMany(right => right.Companies)
                .HasForeignKey(foreign => foreign.ExchangeId);

            builder.ToTable("Company", "stock");
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.ShortName).HasColumnName("ShortName");
        }
    }
}
