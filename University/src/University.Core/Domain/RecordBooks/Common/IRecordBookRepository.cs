using University.Core.Domain.RecordBooks.Models;

namespace University.Core.Domain.RecordBooks.Common;

public interface IRecordBookRepository
{
    Task<RecordBook> FindAsync(Guid id);
    
    Task AddAsync(RecordBook recordBook);

    Task DeleteAsync(Guid id);
}