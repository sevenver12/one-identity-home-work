using Microsoft.EntityFrameworkCore;
using OneIdentity.Homework.Repository.Models;

namespace OneIdentity.Homework.Repository.Extensions;
public static class QueryableExtensions
{
    public static async Task<PagedCollection<T>> ToPagedCollectionAsync<T>(this IQueryable<T> query, int pageSize,
                                                                           int pageNumber,
                                                                           CancellationToken cancellationToken = default) 
                                                                            where T : class
    {
        var result = await query.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PagedCollection<T>
        {
            CurrentPage = pageNumber,
            Items = result,
        };
    }
}
