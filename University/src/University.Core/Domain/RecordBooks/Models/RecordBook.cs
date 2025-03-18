using University.Core.Common;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.RecordBooks.Validators;
using University.Core.Domain.Students.Models;

namespace University.Core.Domain.RecordBooks.Models;

public class RecordBook : Entity
{
    private RecordBook()
    {

    }

    public RecordBook(Guid id, Guid studentId)
    {
        Id = id;
        StudentId = studentId;
    }

    public Guid Id { get; private set; }

    public Guid StudentId { get; private set; }

    public Student Student { get; private set; }

    public IReadOnlyCollection<RecordBookSubject> Subjects { get; private set; }

    public static async Task<RecordBook> CreateAsync(
        Guid studentId,
        IStudentIdMustBeUniqueChecker studentIdMustBeUniqueChecker,
        CancellationToken cancellationToken = default)
    {
        var recordBook = new RecordBook(new Guid(), studentId);
        await ValidateAsync(new CreateRecordBookDataValidator(null, studentIdMustBeUniqueChecker), recordBook, cancellationToken);
        return recordBook;
    }
}