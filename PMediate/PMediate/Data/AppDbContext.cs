using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMediate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMediate.Data
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public static readonly ILoggerFactory ConsoleLoggerFactory
             = LoggerFactory.Create(b => b.AddConsole());

        public DbSet<Consult> Consults { get; private set; }
    }
}
