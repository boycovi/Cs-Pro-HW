using University.Core.Domain.Teachers.Models;

namespace University.Core.Domain.Teachers.Common;

public interface ITeacherSubjectsRepository
{
    Task<TeacherSubject> FindAsync(Guid teacherId);

    Task AddAsync(TeacherSubject teacherSubject);

    Task DeleteAsync(Guid teacherId);
}