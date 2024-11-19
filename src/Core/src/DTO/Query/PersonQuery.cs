
namespace VozAmiga.Core.DTO.Query;

public record PersonQuery(string? filter, int page = 0, int itensPerpage = 25, string? orderBy = null);
