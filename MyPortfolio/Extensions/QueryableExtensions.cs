using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Common;

namespace MyPortfolio.Extensions
{
    /// <summary>
    /// Extension methods for IQueryable to support pagination, sorting, and filtering
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Apply pagination to a queryable
        /// </summary>
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);
            
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Apply dynamic sorting to a queryable
        /// </summary>
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> query,
            string? sortBy,
            bool isDescending = false)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = typeof(T).GetProperty(sortBy);

            if (property == null)
                return query;

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var methodName = isDescending ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), property.PropertyType },
                query.Expression,
                Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        /// <summary>
        /// Apply search filter across multiple string properties
        /// </summary>
        public static IQueryable<T> ApplySearch<T>(
            this IQueryable<T> query,
            string? searchTerm,
            params Expression<Func<T, string>>[] properties)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || properties.Length == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? predicate = null;

            foreach (var property in properties)
            {
                var propertyExpression = Expression.Invoke(property, parameter);
                var nullCheck = Expression.NotEqual(propertyExpression, Expression.Constant(null));
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsExpression = Expression.Call(
                    propertyExpression,
                    containsMethod!,
                    Expression.Constant(searchTerm));

                var condition = Expression.AndAlso(nullCheck, containsExpression);

                predicate = predicate == null
                    ? condition
                    : Expression.OrElse(predicate, condition);
            }

            if (predicate != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            return query;
        }
    }
}
