using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Core.Models;

namespace Harvested.Quotes.Data
{
    public interface IQuotesDBContext
    {
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbSet<Quote> Quotes { get; set; }
        DbSet<MessageLog> MessageLogs { get; set; }
    }
}
