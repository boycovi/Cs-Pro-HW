namespace University.Core.Domain.Faculties.Common;

public interface IFacultyNameMustBeUniqueChecker
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}