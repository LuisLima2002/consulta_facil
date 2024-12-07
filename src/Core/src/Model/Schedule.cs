
namespace VozAmiga.Core.Data.Model;

public class Schedule
{
    public Schedule(Guid id){
        Id = id;
    }
    public Schedule(){ }

    public Guid Id { get; set; }
    public required Guid PatientId { get; set; }
    public required string PatientName { get; set; }
    public required DateTime Date { get; set; }
    public required string ScheduleType { get; set; }
    public required string Reason { get; set; }
}
