using University.Core.Domain.RecordBooks.Models;

namespace University.Core.Domain.RecordBooks.Common;

public interface IMarkRepository
{
    Task<RecordBookSubject> FindAsync(Guid recordId);

    Task AddAsync(RecordBookSubject recordBookSubject);

    Task DeleteAsync(Guid recordId);
}