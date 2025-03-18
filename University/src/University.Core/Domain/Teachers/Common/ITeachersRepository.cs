using University.Core.Domain.Teachers.Models;

namespace University.Core.Domain.Teachers.Common;

public interface ITeachersRepository
{
    Task<Teacher> FindAsync(Guid id);

    Task AddAsync(Teacher teacher);

    Task DeleteAsync(Guid id);
}