using University.Core.Common;
using University.Core.Domain.Teachers.Common;

namespace University.Core.Domain.Teachers.Rules;

public class TeacherFirstNameMustBeInRangeRule : IBusinessRuleAsync
{
    private readonly string _firstName;
    private readonly ITeacherFirstNameMustBeInRangeChecker _teacherFirstNameMustBeInRangeChecker;

    public TeacherFirstNameMustBeInRangeRule(
        string firstName, 
        ITeacherFirstNameMustBeInRangeChecker teacherFirstNameMustBeInRangeChecker)
    {
        _firstName = firstName;
        _teacherFirstNameMustBeInRangeChecker = teacherFirstNameMustBeInRangeChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isValid = await _teacherFirstNameMustBeInRangeChecker.IsValidAsync(_firstName, cancellationToken);
        return Check(isValid);
    }

    private RuleResult Check(bool isValid)
    {
        if (isValid) return RuleResult.Success();
        return RuleResult.Failed($"Teacher first name: '{_firstName}' must be in the range from 2 to 50.");
    }
}