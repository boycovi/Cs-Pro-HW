using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Subjects.Common;
using University.Persistence.UniversityDb;
using University.Core.Domain.Subjects.Models;

namespace University.Infrastructure.Core.Domain.Subjects.Common;

public class SubjectsRepository : ISubjectsRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public SubjectsRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Subject> FindAsync(Guid id)
    {
        var subject = await _universityDbContext.Subjects.SingleOrDefaultAsync(x => x.Id == id);

        return subject ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Subject subject)
    {
        await _universityDbContext.Subjects.AddAsync(subject);
    }

    public async Task DeleteAsync(Guid id)
    {
        var subjectToBeRemoved = await _universityDbContext.Subjects.SingleOrDefaultAsync(subject => subject.Id == id);
        if (subjectToBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.Subjects.Remove(subjectToBeRemoved);
    }
}
