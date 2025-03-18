using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Subjects.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Subjects.Common;

public class SubjectNameMustBeUniqueChecker : ISubjectNameMustBeUniqueChecker
{
    private readonly UniversityDbContext _dbContext;

    public SubjectNameMustBeUniqueChecker(UniversityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Subjects
            .AsNoTracking()
            .AllAsync(contact => contact.Name != name, cancellationToken);
    }
}