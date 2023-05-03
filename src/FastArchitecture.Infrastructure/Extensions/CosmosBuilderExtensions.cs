using FastArchitecture.Domain.Abstractions;
using FastArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace FastArchitecture.Infrastructure;

internal static class CosmosBuilderExtensions
{
    public static EntityTypeBuilder<T> SetDefaults<T>(this EntityTypeBuilder<T> builder) where T : Entity
    {
        var containerName = typeof(ApplicationDbContext)
            .GetProperties()
            .Where(x => x.PropertyType == typeof(DbSet<T>))
            .Select(x => x.Name)
            .Single();

        builder.HasKey(x => x.Id);
        builder.HasNoDiscriminator();
        builder.Property(x => x.Id).ToJsonProperty("id");
        builder.ToContainer(containerName);

        return builder;
    }

    public static EntityTypeBuilder<T> SetDefaults<T>(this EntityTypeBuilder<T> builder, Expression<Func<T, string>> partitionKey) where T : Entity
    {
        builder.SetDefaults();
        builder.HasPartitionKey(partitionKey);

        return builder;
    }

    public static EntityTypeBuilder<T> SetDefaults<T>(this EntityTypeBuilder<T> builder, Expression<Func<T, Guid>> partitionKey) where T : Entity
    {
        builder.SetDefaults();
        builder.HasPartitionKey(partitionKey);

        return builder;
    }
}