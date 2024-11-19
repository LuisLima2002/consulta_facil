

namespace VozAmiga.Core.DTO.ViewModels;

public record PatientQR
{

    public required string Id { get; set; }

    public required string Name { get; set; }
    public required string Gender { get; set; }
    public required DateTime Birthdate { get; set; }
    public required string Address { get; set; }

    public required string PhoneNumber { get; set; }
    public required string Document { get; set; }
    public required string HealthInsurance { get; set; }
    public DateTime? DeathDay { get; set; }
    public DateTime? DeathReason { get; set; }

}
