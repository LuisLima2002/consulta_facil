
namespace VozAmiga.Core.DTO.Query;

public record Query(string? Filter, int Page = 0, int ItemsPerPage = 25, string? OrderBy = null);
