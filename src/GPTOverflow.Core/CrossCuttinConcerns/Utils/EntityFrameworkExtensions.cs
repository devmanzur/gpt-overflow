using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using GPTOverflow.Core.CrossCuttinConcerns.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GPTOverflow.Core.CrossCuttinConcerns.Utils;

public static class EntityFrameworkExtensions
{
    /// <summary>
    /// Adds a softDeleted item ignore condition on top of the conditions provided by the query
    /// Meaning entries from Classes extending from ISoftDeletable that have been deleted ( Soft Deleted )
    /// will not show up in the result
    /// </summary>
    /// <param name="entityData"></param>
    public static void AddIgnoreSoftDeletedItemQueryFilter(
        this IMutableEntityType entityData)
    {
        var methodToCall = typeof(EntityFrameworkExtensions)
            .GetMethod(nameof(GetSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            ?.MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall?.Invoke(null, Array.Empty<object>());
        entityData.SetQueryFilter((LambdaExpression)filter!);
        entityData.AddIndex(entityData.FindProperty(nameof(ISoftDeletable.IsSoftDeleted))!);
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>()
        where TEntity : class, ISoftDeletable
    {
        Expression<Func<TEntity, bool>> filter = x => !x.IsSoftDeleted;
        return filter;
    }

    /// <summary>
    /// Syntactic sugar over AsNoTracking for queries
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static IQueryable<TEntity> ReadOnly<TEntity>(
        [NotNull] this IQueryable<TEntity> source)
        where TEntity : class
    {
        return source.AsNoTracking();
    }

    /// <summary>
    /// Syntactic sugar over FirstOrDefaultAsync for queries
    /// </summary>
    /// <param name="source"></param>
    /// <param name="id"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static Task<TEntity?> FindById<TEntity>(
        [NotNull] this IQueryable<TEntity> source, Guid id)
        where TEntity : IEntity
    {
        return source.FirstOrDefaultAsync(x => x.Id == id);
    }

    public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(int id)
    {
        var item = Expression.Parameter(typeof(TEntity), "entity");
        var prop = Expression.Property(item, typeof(TEntity).Name[..^1] + "Id");
        var value = Expression.Constant(id);
        var equal = Expression.Equal(prop, value);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);
        return lambda;
    }
}