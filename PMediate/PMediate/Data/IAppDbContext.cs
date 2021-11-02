using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PMediate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PMediate.Data
{
    public interface IAppDbContext
    {
        DbSet<Consult> Consults { get; }

        EntityEntry Add(object entity);
        EntityEntry Remove(object entity);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class;
        ChangeTracker ChangeTracker { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
