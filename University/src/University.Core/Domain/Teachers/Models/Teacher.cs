using University.Core.Common;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Data;
using University.Core.Domain.Teachers.Validators;

namespace University.Core.Domain.Teachers.Models;

public class Teacher : Entity
{
    private Teacher()
    {

    }

    public Teacher(Guid id, string firstName, string lastName, string middleName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public Guid Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public IReadOnlyCollection<TeacherSubject> Subjects { get; private set; }

    public static Teacher Create(string firstName, string lastName, string middleName)
    {
        return new Teacher(Guid.NewGuid(), firstName, lastName, middleName);
    }

    public async Task UpdateAsync(
        UpdateTeacherData data,
        ITeacherFirstNameMustBeInRangeChecker teacherFirstNameMustBeInRangeChecker,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new UpdateTeacherDataValidator(this, teacherFirstNameMustBeInRangeChecker), data, cancellationToken);

        FirstName = data.FirstName;
        LastName = data.LastName;
        MiddleName = data.MiddleName;
    }
}