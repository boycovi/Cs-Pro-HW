using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Faculties.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Faculties.Common;

public class FacultyDepartmentRepository : IFacultyDepartmentRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public FacultyDepartmentRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<FacultyDepartment> FindAsync(Guid facultyId)
    {
        var faculty = await _universityDbContext.FacultyDepartments.SingleOrDefaultAsync(x => x.FacultyId == facultyId);

        return faculty ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(FacultyDepartment facultyDepartment)
    {
       await _universityDbContext.FacultyDepartments.AddAsync(facultyDepartment);
    }

    public async Task DeleteAsync(Guid facultyId)
    {
        var facultyBeRemoved = await _universityDbContext.FacultyDepartments.SingleOrDefaultAsync(x => x.FacultyId == facultyId);
        if (facultyBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.FacultyDepartments.Remove(facultyBeRemoved);
    }
}