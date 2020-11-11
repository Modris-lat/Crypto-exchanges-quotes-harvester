using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Harvested.Quotes.Data
{
    public class QuotesDBContext: DbContext
    {
        public QuotesDBContext() : base("url-storage")
        {
            Database.SetInitializer<QuotesDBContext>(null);
            //Database.SetInitializer<QuotesDBContext>(
            //    new MigrateDatabaseToLatestVersion<QuotesDBContext, Configuration
            //    >());
        }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<MessageLog> MessageLogs { get; set; }
    }
}
