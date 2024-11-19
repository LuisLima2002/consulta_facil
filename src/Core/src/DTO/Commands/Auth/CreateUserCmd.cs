
namespace VozAmiga.Core.DTO.Commands;

public record CreateUserCmd
{
    public required string Name { get; set; }
    public required string Birthday { get; set; }
    public required string EmergencyContact { get; set; }
    public required string CPFPatient { get; set; }
    public required string NameResponsible { get; set; }
    public required string CPFResponsible { get; set; }
    public string? Diagnosis { get; set; }

};
