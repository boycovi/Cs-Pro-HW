using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Departments.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Departments.Common;

public class DepartmentNameMustBeUniqueChecker : IDepartmentNameMustBeUniqueChecker
{
    private readonly UniversityDbContext _dbContext;

    public DepartmentNameMustBeUniqueChecker(UniversityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments
            .AsNoTracking()
            .AllAsync(contact => contact.Name != name, cancellationToken);
    }
}
