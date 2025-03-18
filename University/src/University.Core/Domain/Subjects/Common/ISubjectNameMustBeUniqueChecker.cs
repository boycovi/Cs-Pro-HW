namespace University.Core.Domain.Subjects.Common;

public interface ISubjectNameMustBeUniqueChecker
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}