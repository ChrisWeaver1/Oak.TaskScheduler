using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Scheduler.Example
{
    public class DatabaseContext : DbContext
    {
        private readonly IOptions<DbOptions> options;

        public DatabaseContext(IOptions<DbOptions> options)
        {
            this.options = options;
        }

        public DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(this.options.Value.ConnectionString);
        }
    }
}