namespace OneIdentity.Homework.Repository.Models;
public class PagedCollection<T> where T : class
{
    /// <summary>
    /// The underlaying items
    /// </summary>
    public required IEnumerable<T> Items { get; set; }

    /// <summary>
    /// The current page of the pagination
    /// </summary>
    public required int CurrentPage { get; set; }

    /// <summary>
    /// Size of the page
    /// </summary>
    public int PageSize { get => Items.Count(); }

}
