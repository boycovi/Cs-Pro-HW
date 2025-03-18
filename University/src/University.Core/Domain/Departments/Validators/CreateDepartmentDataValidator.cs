using FluentValidation;
using FluentValidation.Results;
using University.Core.Domain.Departments.Common;
using University.Core.Domain.Departments.Models;
using University.Core.Domain.Departments.Rules;

namespace University.Core.Domain.Departments.Validators;

public class CreateDepartmentDataValidator : AbstractValidator<Department>
{
    public CreateDepartmentDataValidator(
        Department department,
        IDepartmentNameMustBeUniqueChecker departmentNameMustBeUniqueChecker)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (department != null && department.Name == name) return;
                var checkResult = await new DepartmentNameMustBeUniqueRule(name, departmentNameMustBeUniqueChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(Department.Name), error));
                }
            });
    }
}