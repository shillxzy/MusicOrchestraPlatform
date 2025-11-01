using CatalogService.DAL.Helpers;
using CatalogService.Domain.Entities.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Extensions
{
    public static class QueryablePagingExtensions
    {
        public static Task<PagedList<T>> ToPagedListAsync<T, TParams>(
            this IQueryable<T> query,
            TParams parameters,
            CancellationToken cancellationToken = default)
            where TParams : QueryStringParameters
        {
            return PagedList<T>.ToPagedListAsync(
                query,
                parameters.PageNumber,
                parameters.PageSize,
                cancellationToken
            );
        }
    }
}
