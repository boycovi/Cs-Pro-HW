using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.Faculties.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations
{
    public class FacultyEntityConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Metadata
                .FindNavigation(nameof(Faculty.Departments))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany(x => x.Departments)
                .WithOne()
                .HasForeignKey(x => x.FacultyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
