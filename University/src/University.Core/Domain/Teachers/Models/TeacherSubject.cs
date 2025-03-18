using University.Core.Domain.Subjects.Models;

namespace University.Core.Domain.Teachers.Models;

public class TeacherSubject
{
    private TeacherSubject()
    {

    }

    private TeacherSubject(Guid teacherId, Guid subjectId)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
    }

    public Guid TeacherId { get; set; }

    public Guid SubjectId { get; set; }

    public Subject Subject { get; set; }

    public static TeacherSubject Create(Guid teacherId, Guid subjectId)
    {
        return new TeacherSubject(teacherId, subjectId);
    }
}