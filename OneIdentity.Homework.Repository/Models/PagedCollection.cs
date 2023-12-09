namespace OneIdentity.Homework.Repository.Models;
public class PagedCollection<T>
{
    public required IEnumerable<T> Items { get; set; }
    public required int CurrentPage { get; set; }
    public int PageCount { get => Items.Count(); }

}
