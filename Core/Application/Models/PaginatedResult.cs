using System;

namespace Application.Models;

public class PaginatedResult<T>
{
    public IEnumerable<T> Result { get; set; } = new List<T>();

    public int Page { get; set; }
    
    public int PageSize { get; set;}
    
    public int TotalCount { get; set; }
}
