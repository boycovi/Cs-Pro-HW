using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Teachers.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Teachers.Common;

public class TeacherFirstNameMustBeInRangeChecker : ITeacherFirstNameMustBeInRangeChecker
{
    private readonly UniversityDbContext _dbContext;

    public TeacherFirstNameMustBeInRangeChecker(UniversityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsValidAsync(string firstName, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Teachers
            .AsNoTracking()
            .AllAsync(contact => contact.FirstName != firstName, cancellationToken);
    }
}
