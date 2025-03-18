using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.Teachers.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class TeacherEntityConfigurations : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
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

        builder
            .Metadata
            .FindNavigation(nameof(Teacher.Subjects))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.Subjects)
            .WithOne()
            .HasForeignKey(x => x.TeacherId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}