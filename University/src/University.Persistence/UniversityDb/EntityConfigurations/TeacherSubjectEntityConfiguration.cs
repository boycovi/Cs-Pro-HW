using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.Teachers.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class TeacherSubjectEntityConfiguration : IEntityTypeConfiguration<TeacherSubject>
{
    public void Configure(EntityTypeBuilder<TeacherSubject> builder)
    {
        builder.HasKey(x => new { x.TeacherId, x.SubjectId });
    }
}