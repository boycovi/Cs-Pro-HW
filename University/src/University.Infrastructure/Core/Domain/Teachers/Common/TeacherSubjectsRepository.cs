using Microsoft.EntityFrameworkCore;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Models;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Teachers.Common;

public class TeacherSubjectsRepository : ITeacherSubjectsRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public TeacherSubjectsRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<TeacherSubject> FindAsync(Guid teacherId)
    {
        var teacher = await _universityDbContext.TeachersSubjects.SingleOrDefaultAsync(x => x.TeacherId == teacherId);
        return teacher ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(TeacherSubject teacherSubject)
    {
        await _universityDbContext.TeachersSubjects.AddAsync(teacherSubject);
    }

    public async Task DeleteAsync(Guid teacherId)
    {
        var teacherToBeRemoved = await _universityDbContext.TeachersSubjects.SingleOrDefaultAsync(x => x.TeacherId == teacherId);
        if (teacherToBeRemoved is null) throw new InvalidOperationException();
        _universityDbContext.TeachersSubjects.Remove(teacherToBeRemoved);
    }
}