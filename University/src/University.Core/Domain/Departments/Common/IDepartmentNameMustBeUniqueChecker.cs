namespace University.Core.Domain.Departments.Common;

public interface IDepartmentNameMustBeUniqueChecker
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}