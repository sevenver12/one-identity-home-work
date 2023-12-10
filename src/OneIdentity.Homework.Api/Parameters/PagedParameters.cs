using Microsoft.AspNetCore.Mvc;

namespace OneIdentity.Homework.Api.Parameters;

public class PagedParameters
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
