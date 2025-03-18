using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.Students.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class StudentEntityConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.MiddleName)
            .HasMaxLength(250)
            .IsRequired();
    }
}