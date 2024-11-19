


namespace VozAmiga.Api.Utils.Database;

public class QueryConfig<T>
{
    public QueryConfig(
        Page? page = null
    )
    {
        Page = page ?? Page.Default;
    }
    public Page Page { get; init; }
    public required Func<T, bool> Query { get; set; }
    /// <summary>
    /// Full include paths
    /// </summary>
    /// <value></value>
    public List<string>? Includes { get; set; }
}
