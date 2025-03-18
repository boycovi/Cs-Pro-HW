using FluentValidation;
using FluentValidation.Results;
using University.Core.Domain.Groups.Common;
using University.Core.Domain.Groups.Models;
using University.Core.Domain.Groups.Rules;

namespace University.Core.Domain.Groups.Validators;

public class CreateGroupDataValidator : AbstractValidator<Group>
{
    public CreateGroupDataValidator(
        Group group,
    IGroupNameMustBeUniqueChecker groupNameMustBeUniqueChecker)
    {
        RuleFor(x => x.Name)
        .NotNull()
        .CustomAsync(async (name, context, cancellationToken) =>
        {
            if (group != null && group.Name == name) return;
                var checkResult = await new GroupNameMustBeUniqueRule(name, groupNameMustBeUniqueChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(Group.Name), error));
                }
        });
    }
}