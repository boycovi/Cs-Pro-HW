using University.Core.Common;
using University.Core.Domain.Students.Common;

namespace University.Core.Domain.Students.Rules;

public class StudentFirstNameMustBeInRangeRule : IBusinessRuleAsync
{
    private readonly string _firstName;
    private readonly IStudentFirstNameMustBeInRangeChecker _studentNameMustBeInRangeChecker;

    public StudentFirstNameMustBeInRangeRule(
        string firstName,
        IStudentFirstNameMustBeInRangeChecker studentNameMustBeInRangeChecker)
    {
        _firstName = firstName;
        _studentNameMustBeInRangeChecker = studentNameMustBeInRangeChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isValid = await _studentNameMustBeInRangeChecker.IsValidAsync(_firstName, cancellationToken);
        return Check(isValid);
    }

    private RuleResult Check(bool isValid)
    {
        if (isValid) return RuleResult.Success();
        return RuleResult.Failed($"Student first name: '{_firstName}' must be in the range from 2 to 50.");
    }

}