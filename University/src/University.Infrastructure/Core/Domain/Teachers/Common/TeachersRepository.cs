using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Teachers.Common;

public class TeachersRepository : ITeachersRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public TeachersRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Teacher> FindAsync(Guid id)
    {
        var teacher = await _universityDbContext.Teachers.SingleOrDefaultAsync(teacher => teacher.Id == id);
        return teacher ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Teacher teacher)
    {
        await _universityDbContext.Teachers.AddAsync(teacher);
    }

    public async Task DeleteAsync(Guid id)
    {
        var teacherToBeRemoved = await _universityDbContext.Teachers.SingleOrDefaultAsync(teacher => teacher.Id == id);
        if (teacherToBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.Teachers.Remove(teacherToBeRemoved);
    }
}