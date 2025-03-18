using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Groups.Common;
using University.Core.Domain.Groups.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Groups.Common;

public class GroupRepository : IGroupRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public GroupRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Group> FindAsync(Guid id)
    {
        var studentGroup = await _universityDbContext.Groups.SingleOrDefaultAsync(x => x.Id == id);
        
        return studentGroup ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Group group)
    {
        await _universityDbContext.Groups.AddAsync(group);
    }

    public async Task DeleteAsync(Guid id)
    {
        var studentGroupToBeRemove = await _universityDbContext.Groups.SingleOrDefaultAsync(x => x.Id == id);
        if (studentGroupToBeRemove is null) throw new InvalidOperationException();
        _universityDbContext.Groups.Remove(studentGroupToBeRemove);
    }
}