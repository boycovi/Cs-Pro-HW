using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Students.Common;
using University.Core.Domain.Students.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Students.Common;

public class StudentsRepository : IStudentsRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public StudentsRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Student> FindAsync(Guid id)
    {
        var student = await _universityDbContext.Students.SingleOrDefaultAsync(student => student.Id == id);
        return student ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Student student)
    {
        await _universityDbContext.Students.AddAsync(student);
    }

    public async Task DeleteAsync(Guid id)
    {
        var studentToBeRemoved = await _universityDbContext.Students.SingleOrDefaultAsync(student => student.Id == id);
        if (studentToBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.Students.Remove(studentToBeRemoved);
    }
}