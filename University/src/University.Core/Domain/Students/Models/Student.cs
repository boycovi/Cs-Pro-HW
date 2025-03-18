using University.Core.Common;
using University.Core.Domain.Groups.Models;
using University.Core.Domain.Students.Common;
using University.Core.Domain.Students.Data;
using University.Core.Domain.Students.Validators;

namespace University.Core.Domain.Students.Models;

public class Student : Entity
{
    private Student()
    {

    }

    public Student(Guid id, string firstName, string lastName, string middleName, Guid groupId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        GroupId = groupId;
    }

    public Guid Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public Guid GroupId { get; private set; }

    public Group Group { get; private set; }

    public static Student Create(string firstName, string lastName, string middleName, Guid groupId)
    {
        return new Student(Guid.NewGuid(), firstName, lastName, middleName, groupId);
    }

    public async Task UpdateAsync(
        UpdateStudentData data,
        IStudentFirstNameMustBeInRangeChecker studentNameMustBeInRange,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new UpdateStudentDataValidator(this, studentNameMustBeInRange), data, cancellationToken);

        FirstName = data.FirstName;
        LastName = data.LastName;
        MiddleName = data.MiddleName;
    }
}