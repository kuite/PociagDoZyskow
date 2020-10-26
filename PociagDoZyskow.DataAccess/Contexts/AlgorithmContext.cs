using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Entities.Algorithms;
using PociagDoZyskow.DataAccess.Mapping.Algorithms;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public sealed class AlgorithmContext : BaseContext
    {
        public DbSet<AlgorithmResult> AlgorithmResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlgorithmResultMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
