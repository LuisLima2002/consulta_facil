
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VozAmiga.Core.Data.Model;

public class ScheduleEntityMap : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");

        builder.HasKey(p=> p.Id);

        builder.Property(a => a.PatientId)
         .IsRequired();

        builder.Property(a => a.PatientName)
 .IsRequired();

        builder.Property(a => a.Date)
         .IsRequired();

        builder.Property(a => a.ScheduleType)
         .IsRequired();
    }

}
