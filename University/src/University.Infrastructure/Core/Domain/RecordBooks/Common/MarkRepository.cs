using Microsoft.EntityFrameworkCore;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.RecordBooks.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.RecordBooks.Common;

public class MarkRepository : IMarkRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public MarkRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<RecordBookSubject> FindAsync(Guid recordId)
    {
        var record = await _universityDbContext.RecordBooksSubjects.SingleOrDefaultAsync(x => x.RecordId == recordId);
        return record ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(RecordBookSubject recordBookSubject)
    {
        await _universityDbContext.RecordBooksSubjects.AddAsync(recordBookSubject);
    }

    public async Task DeleteAsync(Guid recordId)
    {
        var recordToBeRemoved = await _universityDbContext.RecordBooksSubjects.SingleOrDefaultAsync(x => x.RecordId == recordId);
        if (recordToBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.RecordBooksSubjects.Remove(recordToBeRemoved);
    }
}