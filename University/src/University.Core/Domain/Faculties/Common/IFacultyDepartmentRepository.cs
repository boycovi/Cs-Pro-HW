using University.Core.Domain.Faculties.Models;

namespace University.Core.Domain.Faculties.Common;

public interface IFacultyDepartmentRepository
{
    Task<FacultyDepartment> FindAsync(Guid facultyId);

    Task AddAsync(FacultyDepartment facultyDepartment);

    Task DeleteAsync(Guid facultyId);
}