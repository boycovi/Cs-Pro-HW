using University.Core.Common;
using University.Core.Domain.Subjects.Common;

namespace University.Core.Domain.Subjects.Rules;

public class SubjectNameMustBeUniqueRule : IBusinessRuleAsync
{
    private readonly string _name;
    private readonly ISubjectNameMustBeUniqueChecker _nameMustBeUniqueChecker;

    public SubjectNameMustBeUniqueRule(string name, ISubjectNameMustBeUniqueChecker nameMustBeUniqueChecker)
    {
        _name = name;
        _nameMustBeUniqueChecker = nameMustBeUniqueChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await _nameMustBeUniqueChecker.IsUniqueAsync(_name, cancellationToken);
        return Check(isUnique);
    }

    private RuleResult Check(bool isUnique)
    {
        if (isUnique) return RuleResult.Success();
        return RuleResult.Failed($"Subject name: '{_name}' must be unique.");
    }
}