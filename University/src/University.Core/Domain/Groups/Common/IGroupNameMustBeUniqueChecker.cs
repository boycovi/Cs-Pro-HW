namespace University.Core.Domain.Groups.Common;

public interface IGroupNameMustBeUniqueChecker
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}