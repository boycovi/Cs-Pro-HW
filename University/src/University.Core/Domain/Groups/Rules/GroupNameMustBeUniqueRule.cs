using University.Core.Common;
using University.Core.Domain.Groups.Common;

namespace University.Core.Domain.Groups.Rules;

public class GroupNameMustBeUniqueRule : IBusinessRuleAsync
{
    private readonly string _name;
    private readonly IGroupNameMustBeUniqueChecker _groupNameMustBeUniqueChecker;

    public GroupNameMustBeUniqueRule(string name, IGroupNameMustBeUniqueChecker groupNameMustBeUniqueChecker)
    {
        _name = name;
        _groupNameMustBeUniqueChecker = groupNameMustBeUniqueChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await _groupNameMustBeUniqueChecker.IsUniqueAsync(_name, cancellationToken);
        return Check(isUnique);
    }

    private RuleResult Check(bool isUnique)
    {
        if (isUnique) return RuleResult.Success();
        return RuleResult.Failed($"Group name: '{_name}' must be unique.");
    }
}