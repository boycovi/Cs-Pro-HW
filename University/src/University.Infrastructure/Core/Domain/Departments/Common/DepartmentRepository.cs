using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Departments.Common;
using University.Core.Domain.Departments.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Departments.Common;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public DepartmentRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Department> FindAsync(Guid id)
    {
        var department = await _universityDbContext.Departments.SingleOrDefaultAsync(x => x.Id == id);
        return department ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Department department)
    {
        await _universityDbContext.Departments.AddAsync(department);
    }

    public async Task DeleteAsync(Guid id)
    {
        var departmentBeRemoved = await _universityDbContext.Departments.SingleOrDefaultAsync(x => x.Id == id);
        if (departmentBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.Departments.Remove(departmentBeRemoved);
    }
}