namespace University.Core.Domain.RecordBooks.Common;

public interface IStudentIdMustBeUniqueChecker
{
    Task<bool> IsValidRangeAsync(Guid studentId, CancellationToken cancellationToken = default);
}