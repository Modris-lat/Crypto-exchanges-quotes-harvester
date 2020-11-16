using System.Data.Entity;
using System.Threading.Tasks;
using Core.Models;
using Harvested.Quotes.Data.Migrations;

namespace Harvested.Quotes.Data
{
    public class QuotesDBContext: DbContext, IQuotesDBContext
    {
        public QuotesDBContext() : base("quotes-storage")
        {
            Database.SetInitializer<QuotesDBContext>(
                new MigrateDatabaseToLatestVersion<QuotesDBContext, Configuration
                >());
        }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<MessageLog> MessageLogs { get; set; }
    }
}
