using FluentValidation;
using FluentValidation.Results;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Data;
using University.Core.Domain.Teachers.Models;
using University.Core.Domain.Teachers.Rules;

namespace University.Core.Domain.Teachers.Validators;

public class UpdateTeacherDataValidator : AbstractValidator<UpdateTeacherData>
{
    public UpdateTeacherDataValidator(
        Teacher teacher,
        ITeacherFirstNameMustBeInRangeChecker teacherFirstNameMustBeInRangeChecker)
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .WithName(nameof(UpdateTeacherData.FirstName))
            .MinimumLength(2)
            .MaximumLength(50)
            .CustomAsync(async (firstName, context, cancellationToken) =>
            {
                if (teacher.FirstName == firstName || firstName is null) return;
                var checkResult =
                    await new TeacherFirstNameMustBeInRangeRule(firstName, teacherFirstNameMustBeInRangeChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(UpdateTeacherData.FirstName), error));
                }
            });

    }
}