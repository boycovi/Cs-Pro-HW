using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Students.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Students.Common;

public class StudentFirstNameMustBeInRangeChecker : IStudentFirstNameMustBeInRangeChecker
{
    private readonly UniversityDbContext _dbContext;

    public StudentFirstNameMustBeInRangeChecker(UniversityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsValidAsync(string firstName, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .AsNoTracking()
            .AllAsync(contact => contact.FirstName != firstName, cancellationToken);
    }
}