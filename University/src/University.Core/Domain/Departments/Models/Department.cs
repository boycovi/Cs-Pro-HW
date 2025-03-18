using University.Core.Common;
using University.Core.Domain.Departments.Common;
using University.Core.Domain.Departments.Validators;

namespace University.Core.Domain.Departments.Models;

public class Department : Entity
{
    private Department()
    {

    }

    public Department(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }

    public string Name { get; set; }

    public static async Task<Department> CreateAsync(
        string name,
        IDepartmentNameMustBeUniqueChecker departmentNameMustBeUniqueChecker,
        CancellationToken cancellationToken = default)
    {
        var department = new Department(new Guid(), name);
        await ValidateAsync(new CreateDepartmentDataValidator(null, departmentNameMustBeUniqueChecker), department, cancellationToken);
        return department;
    }
}