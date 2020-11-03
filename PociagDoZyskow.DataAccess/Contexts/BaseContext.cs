using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public class BaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:database"]);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                foreach (var sqlError in dbUpdateConcurrencyException.Data.Keys)
                {
                    Console.WriteLine($"Error Property Name {0} : Error Message: {1}", sqlError, dbUpdateConcurrencyException.Data[sqlError]);
                }

                throw;
            }
            catch (DbUpdateException dbUpdateException)
            {
                foreach (var sqlError in dbUpdateException.Data.Keys)
                {
                    Console.WriteLine($"Error Property Name {0} : Error Message: {1}", sqlError, dbUpdateException.Data[sqlError]);
                }

                throw;
            }
            catch
            {
                throw;
            }
        }
    }
}
