using FluentValidation;
using FluentValidation.Results;
using University.Core.Domain.Subjects.Common;
using University.Core.Domain.Subjects.Data;
using University.Core.Domain.Subjects.Models;
using University.Core.Domain.Subjects.Rules;

namespace University.Core.Domain.Subjects.Validators;

public class UpdateSubjectDataValidator : AbstractValidator<UpdateSubjectData>
{
    public UpdateSubjectDataValidator(
        Subject subject,
        ISubjectNameMustBeUniqueChecker nameMustBeUniqueChecker)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithName(nameof(UpdateSubjectData.Name))
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if (subject.Name == name || name is null) return;
                var checkResult = await new SubjectNameMustBeUniqueRule(name, nameMustBeUniqueChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(UpdateSubjectData.Name), error));
                }
            });
    }
}