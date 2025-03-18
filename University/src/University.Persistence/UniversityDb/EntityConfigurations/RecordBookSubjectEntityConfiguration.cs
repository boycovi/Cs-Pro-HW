using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.RecordBooks.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class RecordBookSubjectEntityConfiguration : IEntityTypeConfiguration<RecordBookSubject>
{
    public void Configure(EntityTypeBuilder<RecordBookSubject> builder)
    {
        builder.HasKey(x=> new {x.RecordId, x.SubjectId});
    }
}