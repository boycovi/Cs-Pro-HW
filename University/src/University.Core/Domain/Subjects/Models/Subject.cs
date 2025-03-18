using University.Core.Domain.Subjects.Common;
using University.Core.Domain.Subjects.Data;
using University.Core.Common;
using University.Core.Domain.Subjects.Validators;

namespace University.Core.Domain.Subjects.Models;

public class Subject : Entity
{
    private Subject()
    {
    }

    public Subject(Guid id, string name, int code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public Guid Id { get; private set; }

    public string Name { get; set; }

    public int Code { get; set; }

    public static Subject Create(string name, int code)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentOutOfRangeException(nameof(name));
        return new Subject(Guid.NewGuid(), name, code);
    }

    public async Task UpdateAsync(
        UpdateSubjectData data,
        ISubjectNameMustBeUniqueChecker subjectNameMustBeUniqueChecker,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new UpdateSubjectDataValidator(this, subjectNameMustBeUniqueChecker), data, cancellationToken);

        Name = data.Name;
        Code = data.Code;
    }
}