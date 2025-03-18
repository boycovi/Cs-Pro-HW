using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.RecordBooks.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class RecordBookEntityConfiguration : IEntityTypeConfiguration<RecordBook>
{
    public void Configure(EntityTypeBuilder<RecordBook> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Metadata
            .FindNavigation(nameof(RecordBook.Subjects))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.Subjects)
            .WithOne()
            .HasForeignKey(x => x.RecordId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}