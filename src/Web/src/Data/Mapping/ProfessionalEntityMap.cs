
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VozAmiga.Core.Data.Model;

public class ProfissionalEntityMap : IEntityTypeConfiguration<Profissional>
{
    public void Configure(EntityTypeBuilder<Profissional> builder)
    {

        builder.HasKey(p=> p.Id);

        builder.Property(a => a.Name)
         .IsRequired();

        builder.Property(a => a.JobPosition)
         .IsRequired();

        builder.Property(a => a.Salt)
         .IsRequired();

        builder.Property(a => a.Password)
         .IsRequired();

        builder.Property(a => a.Permission)
         .IsRequired();

        builder.Property(a => a.UserName)
         .IsRequired();



    }

}
