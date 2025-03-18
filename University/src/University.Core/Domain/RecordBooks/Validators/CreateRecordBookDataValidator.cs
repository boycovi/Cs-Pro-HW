using FluentValidation;
using FluentValidation.Results;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.RecordBooks.Models;
using University.Core.Domain.RecordBooks.Rules;

namespace University.Core.Domain.RecordBooks.Validators;

public class CreateRecordBookDataValidator : AbstractValidator<RecordBook>
{
    public CreateRecordBookDataValidator(
        RecordBook recordBook,
        IStudentIdMustBeUniqueChecker studentIdMustBeUniqueChecker)
    {
        RuleFor(x => x.StudentId)
            .NotNull()
            .CustomAsync(async (studentId, context, cancellationToken) =>
            {
                if(recordBook != null && recordBook.StudentId == studentId) return;
                var checkResult = await new StudentIdMustBeUniqueRule(studentId, studentIdMustBeUniqueChecker).CheckAsync(cancellationToken);

                if(checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(RecordBook.StudentId), error));
                }
            });
    }
}