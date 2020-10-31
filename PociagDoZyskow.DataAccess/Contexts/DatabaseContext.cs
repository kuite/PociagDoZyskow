using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Mapping;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public sealed class DatabaseContext : BaseContext
    {
        public DbSet<Exchange> Exchanges { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DatabaseContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new ExchangeMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
