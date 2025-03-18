using University.Core.Common;
using University.Core.Domain.Groups.Common;
using University.Core.Domain.Groups.Validators;
using University.Core.Domain.Students.Models;

namespace University.Core.Domain.Groups.Models;

public class Group : Entity
{
    private Group()
    {

    }

    private Group(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public IReadOnlyCollection<Student> Students { get; private set; }

    public static async Task<Group> CreateAsync(
        string name,
        IGroupNameMustBeUniqueChecker groupNameMustBeUniqueChecker,
        CancellationToken cancellationToken = default)
    {
        var group = new Group(new Guid(), name);
        await ValidateAsync(new CreateGroupDataValidator(null, groupNameMustBeUniqueChecker), group, cancellationToken);
        return group;
    }
}