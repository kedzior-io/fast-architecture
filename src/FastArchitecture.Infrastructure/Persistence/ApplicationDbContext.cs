using FastArchitecture.Core.Constants;
using FastArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FastArchitecture.Infrastructure.Persistence;

public interface IDbContext : IDisposable
{
    public DbSet<Order> Orders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string? callerFunction = null, [CallerFilePath] string? callerFile = null);
}

public class ApplicationDbContext : DbContext, IDbContext
{
    public DbSet<Order> Orders { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(ConnectionStrings.Sqlite);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string? callerFunction = null, [CallerFilePath] string? callerFile = null) =>
        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
}