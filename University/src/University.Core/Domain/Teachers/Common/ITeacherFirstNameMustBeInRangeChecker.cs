namespace University.Core.Domain.Teachers.Common;

public interface ITeacherFirstNameMustBeInRangeChecker
{
    Task<bool> IsValidAsync(string firstName, CancellationToken cancellationToken = default);
}