using University.Core.Domain.Departments.Models;

namespace University.Core.Domain.Departments.Common;

public interface IDepartmentRepository
{
    Task<Department> FindAsync(Guid id);

    Task AddAsync(Department department);

    Task DeleteAsync(Guid id);
}