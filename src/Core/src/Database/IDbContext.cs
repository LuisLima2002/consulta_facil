// lang
using System.Diagnostics.CodeAnalysis;
// libs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace VozAmiga.Api.Data.Database;

public interface IDbContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    public ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

    public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

    public DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>() where TEntity : class;
}
