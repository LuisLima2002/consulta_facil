
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VozAmiga.Core.Utils.Enums;

namespace VozAmiga.Core.Data.Model;

//public class PersonEntityMap : IEntityTypeConfiguration<Person>
//{
//    public void Configure(EntityTypeBuilder<Person> builder)
//    {
//        builder
//            .HasKey(x => x.Id)
//            .HasName("person_id");

//        builder
//            .Property(x => x.Name)
//            .HasMaxLength(50)
//            .IsRequired();

//        builder
//            .Property(x => x.Email)
//            .HasMaxLength(150)
//            .IsRequired(false);

//        builder
//            .Property(x => x.Password)
//            .HasMaxLength(128)
//            .IsRequired(false);


//        builder
//            .Property(p => p.Salt)
//            .HasMaxLength(54)
//            .IsRequired(false);

//        builder
//            .Property(x => x.Status)
//            .HasDefaultValue(EPersonStatus.Active)
//            .IsRequired(true);

//    }
//}
