
namespace VozAmiga.Core.DTO.Query;

public record Query
{
    public Query(string? filter, int? page = 0, int? itensPerpage = 25, string? orderBy = null)
    {
        Filter = filter;
        Page = page ?? 0;
        ItemsPerPage = itensPerpage ?? 25;
        OrderBy = orderBy;
    }

    public string? Filter { get; }
    public int Page { get; }
    public int ItemsPerPage { get; }
    public string? OrderBy { get; }
}
