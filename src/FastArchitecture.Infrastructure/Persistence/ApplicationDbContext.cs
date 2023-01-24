using FastArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace FastArchitecture.Infrastructure.Persistence;

public interface IDbContext : IDisposable
{
    public DbSet<Order> Orders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string callerFunction = null, [CallerFilePath] string callerFile = null);
}

public class ApplicationDbContext : DbContext, IDbContext
{
    public DbSet<Order> Orders { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string callerFunction = null, [CallerFilePath] string callerFile = null) =>
        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    public void Dispose()
    {
    }
}

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.SetDefaults(x => x.Id);
    }
}