using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.Subjects.Models;
using University.Core.Domain.Teachers.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class SubjectEntityConfigurations : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .HasMany<TeacherSubject>()
            .WithOne(x => x.Subject)
            .HasForeignKey(x => x.SubjectId)
            .IsRequired();
    }
}