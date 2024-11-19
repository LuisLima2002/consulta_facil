

namespace VozAmiga.Core.DTO.ViewModels;

public record ProfissionalQR
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string JobPosition { get; set; }
    public required string Permission { get; set; }
}
