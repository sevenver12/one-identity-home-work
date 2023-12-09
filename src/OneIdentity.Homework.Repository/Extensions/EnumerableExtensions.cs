using OneIdentity.Homework.Repository.Models;

namespace OneIdentity.Homework.Repository.Extensions;
public static class EnumerableExtensions
{
    /// <summary>
    /// Converts an <see cref="IEnumerable{T}/> to a <see cref="PagedCollection{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of the elements of the collection</typeparam>
    /// <param name="items">The collection to be converted to <see cref="PagedCollection{T}"/></param>
    /// <param name="pageNumber">Represents the current page number</param>
    /// <returns>The paged collection of items</returns>
    public static PagedCollection<T> ToPagedCollection<T>(this IEnumerable<T> items, int pageNumber) where T : class
    {
        return new PagedCollection<T>
        {
            CurrentPage = pageNumber,
            Items = items,
        };
    }

}
