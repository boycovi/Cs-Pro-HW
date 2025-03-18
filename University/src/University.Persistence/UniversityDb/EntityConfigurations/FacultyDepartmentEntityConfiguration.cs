using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Domain.Faculties.Models;

namespace University.Persistence.UniversityDb.EntityConfigurations;

public class FacultyDepartmentEntityConfiguration : IEntityTypeConfiguration<FacultyDepartment>
{
    public void Configure(EntityTypeBuilder<FacultyDepartment> builder)
    {
        builder.HasKey(x => new { x.FacultyId, x.DepartmenttId });
    }
}