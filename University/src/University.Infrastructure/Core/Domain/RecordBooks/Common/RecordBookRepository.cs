using Microsoft.EntityFrameworkCore;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.RecordBooks.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.RecordBooks.Common;

public class RecordBookRepository : IRecordBookRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public RecordBookRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<RecordBook> FindAsync(Guid id)
    {
        var recordBook = await _universityDbContext.RecordBooks.SingleOrDefaultAsync(x => x.Id == id);
        return recordBook ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(RecordBook recordBook)
    {
        await _universityDbContext.RecordBooks.AddAsync(recordBook);
    }

    public async Task DeleteAsync(Guid id)
    {
        var recordBookToBeRemoved = await _universityDbContext.RecordBooks.SingleOrDefaultAsync(x => x.Id == id);
        if (recordBookToBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.RecordBooks.Remove(recordBookToBeRemoved);
    }
}