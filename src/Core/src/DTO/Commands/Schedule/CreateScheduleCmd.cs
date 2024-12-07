namespace VozAmiga.Core.DTO.Commands;

public record CreateScheduleCmd
{
    public required string PatientInfo { get; set; }
    public required DateTime Date { get; set; }
    public required string ScheduleType { get; set; }
    public required string Reason { get; set; }
};
