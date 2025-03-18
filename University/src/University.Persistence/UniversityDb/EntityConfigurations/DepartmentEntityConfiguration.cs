using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.Departments.Models;
using University.Core.Domain.Faculties.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .HasMany<FacultyDepartment>()
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmenttId)
            .IsRequired();
    }
}