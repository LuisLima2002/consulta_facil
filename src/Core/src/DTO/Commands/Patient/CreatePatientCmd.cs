namespace VozAmiga.Core.DTO.Commands;

public record CreatePatientCmd
{

    public required string Name { get; set; }
    public required string Gender { get; set; }
    public required DateTime Birthdate { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Document { get; set; }
    public required string HealthInsurance { get; set; }

};
