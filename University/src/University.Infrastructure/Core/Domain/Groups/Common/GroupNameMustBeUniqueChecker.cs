using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Groups.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Groups.Common;

public class GroupNameMustBeUniqueChecker : IGroupNameMustBeUniqueChecker
{
    private readonly UniversityDbContext _dbContext;

    public GroupNameMustBeUniqueChecker(UniversityDbContext dbContext)
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