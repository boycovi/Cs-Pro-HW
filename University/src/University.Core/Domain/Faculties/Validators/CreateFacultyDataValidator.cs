using FluentValidation;
using FluentValidation.Results;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Faculties.Models;
using University.Core.Domain.Faculties.Rules;
using University.Core.Domain.Groups.Models;

namespace University.Core.Domain.Faculties.Validators;

public class CreateFacultyDataValidator : AbstractValidator<Faculty>
{
    public CreateFacultyDataValidator(
        Faculty faculty,
        IFacultyNameMustBeUniqueChecker facultyNameMustBeUniqueChecker)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (faculty != null && faculty.Name == name) return;
                var checkResult = await new FacultyNameMustBeUniqueRule(name, facultyNameMustBeUniqueChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(Group.Name), error));
                }
            });
    }
}