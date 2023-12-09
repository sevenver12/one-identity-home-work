namespace OneIdentity.Homework.Repository.Extensions;
public static class QueryableExtensions
{
    /// <summary>
    /// Applies paging to the provided <see cref="IQueryable{T}"/>
    /// </summary>
    /// <typeparam name="T">Generic type of the query</typeparam>
    /// <param name="query"><see cref="IQueryable{T}"/> to apply the Paging to</param>
    /// <param name="pageSize">The number of items on a page</param>
    /// <param name="pageNumber">The page where the <paramref name="pageSize"/> of elements will be returned from</param>
    /// <returns>Paged <see cref="IQueryable{T}"/> ></returns>
    /// <remarks>If <paramref name="pageSize"/> is 0 then no paging will be applied</remarks>
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int pageSize, int pageNumber) where T : class
    {
        if (pageSize == 0)
        {
            return query;
        }
        return query.Skip(pageNumber * pageSize).Take(pageSize);
    }
}
