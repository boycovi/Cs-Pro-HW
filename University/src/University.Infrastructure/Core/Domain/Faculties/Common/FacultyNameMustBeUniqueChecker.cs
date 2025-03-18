using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Faculties.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Faculties.Common;

public class FacultyNameMustBeUniqueChecker : IFacultyNameMustBeUniqueChecker
{
    private readonly UniversityDbContext _dbContext;

    public FacultyNameMustBeUniqueChecker(UniversityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Groups
            .AsNoTracking()
            .AllAsync(contact => contact.Name != name, cancellationToken);
    }
}