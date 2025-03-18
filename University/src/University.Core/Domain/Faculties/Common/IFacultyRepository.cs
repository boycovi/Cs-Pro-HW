using University.Core.Domain.Faculties.Models;

namespace University.Core.Domain.Faculties.Common;

public interface IFacultyRepository
{
    Task<Faculty> FindAsync(Guid id);

    Task AddAsync(Faculty faculty);

    Task DeleteAsync(Guid id);
}