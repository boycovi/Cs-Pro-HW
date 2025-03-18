using FluentValidation;
using FluentValidation.Results;
using University.Core.Domain.Students.Common;
using University.Core.Domain.Students.Data;
using University.Core.Domain.Students.Models;
using University.Core.Domain.Students.Rules;

namespace University.Core.Domain.Students.Validators;

public class UpdateStudentDataValidator : AbstractValidator<UpdateStudentData>
{
    public UpdateStudentDataValidator(
        Student student,
        IStudentFirstNameMustBeInRangeChecker studentNameMustBeInRange)
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .WithName(nameof(UpdateStudentData.FirstName))
            .MinimumLength(2)
            .MaximumLength(50)
            .CustomAsync(async (firstName, context, cancellationToken) =>
            {
                if (student.FirstName == firstName || firstName is null) return;
                var checkResult =
                    await new StudentFirstNameMustBeInRangeRule(firstName, studentNameMustBeInRange).CheckAsync(
                        cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(UpdateStudentData.FirstName), error));
                }
            });
    }
}