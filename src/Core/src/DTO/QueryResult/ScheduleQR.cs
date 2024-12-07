
namespace VozAmiga.Core.Data.Model;

public class ScheduleQR
{
    public required string Id { get; set; }
    public required string PatientName { get; set; }
    public required string PatientId { get; set; }
    public required DateTime Date { get; set; }
    public required string ScheduleType { get; set; }
    public required string Reason { get; set; }
}
