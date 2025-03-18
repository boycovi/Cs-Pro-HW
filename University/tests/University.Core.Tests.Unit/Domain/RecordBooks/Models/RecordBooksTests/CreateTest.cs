using FluentAssertions;
using Moq;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.RecordBooks.Models;
using University.Core.Exceptions;

namespace University.Core.Tests.Unit.Domain.RecordBooks.Models.RecordBooksTests;

public class CreateTest
{
    private IStudentIdMustBeUniqueChecker StudentIdMustBeUniqueChecker { get; }

    public CreateTest()
    {
        StudentIdMustBeUniqueChecker = Mock.Of<IStudentIdMustBeUniqueChecker>();
    }

    [Fact]
    public async Task Should_create_record_book()
    {
        //Arrange
        Mock.Get(StudentIdMustBeUniqueChecker)
            .Setup(x => x.IsValidRangeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var studentId = Guid.NewGuid();

        //Act
        var recordBook = await RecordBook.CreateAsync(studentId, StudentIdMustBeUniqueChecker);

        //Assert 
        recordBook.Should().NotBeNull();
        recordBook.StudentId.Should().Be(studentId);
    }

    [Fact]
    public async Task When_student_id_is_not_unique_Should_throw_exception()
    {
        //Arrange 
        Mock.Get(StudentIdMustBeUniqueChecker)
            .Setup(x => x.IsValidRangeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var studentId = Guid.NewGuid();

        //Act
        var action = async () => await RecordBook.CreateAsync(studentId, StudentIdMustBeUniqueChecker);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(RecordBook.StudentId));
        failure.ErrorMessage.Should().Be($"Student id: '{studentId}' must be unique.");
    }
}