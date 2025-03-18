namespace University.Core.Domain.Students.Common;

public interface IStudentFirstNameMustBeInRangeChecker
{
    Task<bool> IsValidAsync(string firstName, CancellationToken cancellationToken = default);
}