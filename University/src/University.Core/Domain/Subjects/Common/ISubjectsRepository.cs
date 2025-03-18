using University.Core.Domain.Subjects.Models;

namespace University.Core.Domain.Subjects.Common;

public interface ISubjectsRepository
{
    Task<Subject> FindAsync(Guid id);

    Task AddAsync(Subject subject);

    Task DeleteAsync(Guid id);
}