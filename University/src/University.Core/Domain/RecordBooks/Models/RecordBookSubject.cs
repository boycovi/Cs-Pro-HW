using University.Core.Domain.Subjects.Models;

namespace University.Core.Domain.RecordBooks.Models;

public class RecordBookSubject
{
    private RecordBookSubject()
    {

    }

    private RecordBookSubject(Guid recordId, Guid subjectId, int grade)
    {
        RecordId = recordId;
        SubjectId = subjectId;
        Grade = grade;
    }

    public Guid RecordId { get; set; }

    public Guid SubjectId { get; set; }

    public Subject Subject { get; set; }

    public int Grade { get; set; }

    public static RecordBookSubject Create(Guid recordId, Guid subjectId, int grade)
    {
        return new RecordBookSubject(recordId, subjectId, grade);
    }
}