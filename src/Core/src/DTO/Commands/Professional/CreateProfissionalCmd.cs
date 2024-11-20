namespace VozAmiga.Core.DTO.Commands;

public record CreateProfissionalCmd
{
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string JobPosition { get; set; }
    public required string Permission { get; set; }
    public required string Phone { get; set; }
};
