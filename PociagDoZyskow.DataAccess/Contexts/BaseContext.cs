using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PociagDoZyskow.DataAccess.Contexts
{
    public class BaseContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=PociagDoZyskow;Integrated Security=True");
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
