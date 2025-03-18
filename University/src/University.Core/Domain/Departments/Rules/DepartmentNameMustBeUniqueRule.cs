using University.Core.Common;
using University.Core.Domain.Departments.Common;

namespace University.Core.Domain.Departments.Rules;

public class DepartmentNameMustBeUniqueRule : IBusinessRuleAsync
{
    private readonly string _name;
    private readonly IDepartmentNameMustBeUniqueChecker _departmentNameMustBeUniqueChecker;

    public DepartmentNameMustBeUniqueRule(
        string name, 
        IDepartmentNameMustBeUniqueChecker departmentNameMustBeUniqueChecker)
    {
        _name = name;
        _departmentNameMustBeUniqueChecker = departmentNameMustBeUniqueChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await _departmentNameMustBeUniqueChecker.IsUniqueAsync(_name, cancellationToken);
        return Check(isUnique);
    }

    private RuleResult Check(bool isUnique)
    {
        if (isUnique) return RuleResult.Success();
        return RuleResult.Failed($"Department name: '{_name}' must be unique.");
    }
}