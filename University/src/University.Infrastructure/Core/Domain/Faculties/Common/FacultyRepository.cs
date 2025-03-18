using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Faculties.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Faculties.Common;

public class FacultyRepository : IFacultyRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public FacultyRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Faculty> FindAsync(Guid id)
    {
        var faculty = await _universityDbContext.Faculties.SingleOrDefaultAsync(x => x.Id == id);
        return faculty ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Faculty faculty)
    {
        await _universityDbContext.AddAsync(faculty);
    }

    public async Task DeleteAsync(Guid id)
    {
        var facultyBeRemoved = await _universityDbContext.Faculties.SingleOrDefaultAsync(x => x.Id == id);
        if (facultyBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.Faculties.Remove(facultyBeRemoved);
    }
}