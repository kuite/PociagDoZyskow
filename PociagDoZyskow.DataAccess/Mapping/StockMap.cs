using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PociagDoZyskow.DataAccess.Entities.Stock;

namespace PociagDoZyskow.DataAccess.Mapping
{
    public class StockMap : IEntityTypeConfiguration<Exchange>
    {
        public void Configure(EntityTypeBuilder<Exchange> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.ToTable("Exchange", "stock");
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.ShortName).HasColumnName("ShortName");
        }
    }
}
