using University.Core.Domain.Groups.Models;

namespace University.Core.Domain.Groups.Common;

public interface IGroupRepository
{
    Task<Group> FindAsync(Guid id);

    Task AddAsync(Group group);

    Task DeleteAsync(Guid id);
}