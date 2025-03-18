using Microsoft.EntityFrameworkCore;
using University.Core.Domain.RecordBooks.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.RecordBooks.Common;

public class StudentIdMustBeUniqueChecker : IStudentIdMustBeUniqueChecker
{
    private readonly UniversityDbContext _dbContext;

    public StudentIdMustBeUniqueChecker(UniversityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsValidRangeAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RecordBooks
            .AsNoTracking()
            .AllAsync(contact => contact.StudentId != studentId, cancellationToken);
    }
}