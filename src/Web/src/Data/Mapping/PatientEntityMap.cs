// lib files
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


// interal files
using VozAmiga.Core.Data.Model;

namespace VozAmiga.Core.Data.Repositories;

public class PatientEntityMap : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {

        builder
            .HasKey(x => x.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Birthdate)
            .IsRequired();

        builder.Property(a => a.Address)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Document)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.HealthInsurance)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Gender)
             .IsRequired();

        builder.Property(a => a.PhoneNumber)
             .HasMaxLength(20)
             .IsRequired();

        builder.Property(a => a.DeathDay)
             .IsRequired(false);

        builder.Property(a => a.DeathReason)
                .HasMaxLength(255)
             .IsRequired(false);
    }
}
