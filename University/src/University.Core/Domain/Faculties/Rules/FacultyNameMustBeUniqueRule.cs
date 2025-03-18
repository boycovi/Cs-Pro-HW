using University.Core.Common;
using University.Core.Domain.Faculties.Common;

namespace University.Core.Domain.Faculties.Rules;

public class FacultyNameMustBeUniqueRule : IBusinessRuleAsync
{
    private readonly string _name;
    private readonly IFacultyNameMustBeUniqueChecker _facultyNameMustBeUniqueChecker;

    public FacultyNameMustBeUniqueRule(
        string name, 
        IFacultyNameMustBeUniqueChecker facultyNameMustBeUniqueChecker)
    {
        _name = name;
        _facultyNameMustBeUniqueChecker = facultyNameMustBeUniqueChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await _facultyNameMustBeUniqueChecker.IsUniqueAsync(_name, cancellationToken);
        return Check(isUnique);
    }

    private RuleResult Check(bool isUnique)
    {
        if (isUnique) return RuleResult.Success();
        return RuleResult.Failed($"Faculty name: '{_name}' must be unique.");
    }
}