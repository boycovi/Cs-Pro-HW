using University.Core.Common;
using University.Core.Domain.RecordBooks.Common;

namespace University.Core.Domain.RecordBooks.Rules;

public class StudentIdMustBeUniqueRule : IBusinessRuleAsync
{
    private readonly Guid _studentId;
    private readonly IStudentIdMustBeUniqueChecker _studentIdMustBeUniqueChecker;

    public StudentIdMustBeUniqueRule(Guid studentId, IStudentIdMustBeUniqueChecker studentIdMustBeUniqueChecker)
    {
        _studentId = studentId;
        _studentIdMustBeUniqueChecker = studentIdMustBeUniqueChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await _studentIdMustBeUniqueChecker.IsValidRangeAsync(_studentId, cancellationToken);
        return Check(isUnique);
    }

    private RuleResult Check(bool isUnique)
    {
        if (isUnique) return RuleResult.Success();
        return RuleResult.Failed($"Student id: '{_studentId}' must be unique.");
    }
}