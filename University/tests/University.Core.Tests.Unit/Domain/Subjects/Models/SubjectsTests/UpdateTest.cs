using FluentAssertions;
using Moq;
using University.Core.Domain.Subjects.Common;
using University.Core.Domain.Subjects.Data;
using University.Core.Domain.Subjects.Models;
using University.Core.Exceptions;

namespace University.Core.Tests.Unit.Domain.Subjects.Models.SubjectsTests;

public class UpdateTest
{
    private ISubjectNameMustBeUniqueChecker SubjectNameMustBeUniqueChecker { get; }

    public UpdateTest()
    {
        SubjectNameMustBeUniqueChecker = Mock.Of<ISubjectNameMustBeUniqueChecker>();
    }

    [Fact]
    public async Task Should_update_subject()
    {
        //Arrange 
        Mock.Get(SubjectNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var subject = new Subject(new Guid(), "Tests", 55);
        var data = new UpdateSubjectData("Math", 55);

        //Act
        await subject.UpdateAsync(data, SubjectNameMustBeUniqueChecker);

        //Assert
        subject.Should().NotBeNull();
        subject.Name.Should().Be(data.Name);
    }

    [Fact]
    public async Task When_name_is_not_unique_Should_throw_exception()
    {
        //Arrange 
        Mock.Get(SubjectNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var subject = new Subject(new Guid(), "Tests", 55);
        var data = new UpdateSubjectData("Math", 55);

        //Act
        var action = async () => await subject.UpdateAsync(data, SubjectNameMustBeUniqueChecker);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(UpdateSubjectData.Name));
        failure.ErrorMessage.Should().Be($"{nameof(Subject)} name: '{data.Name}' must be unique.");
    }

    [Theory]
    [InlineData("Tests", "Test")]
    [InlineData("MAth", "Math")]
    [InlineData("Programmer", "Proger")]
    public async Task ManyUpdateExample(string name, string updateName)
    {
        //Arrange
        Mock.Get(SubjectNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var subject = new Subject(new Guid(), name, 99);
        var data = new UpdateSubjectData(updateName, 99);

        //Act
        var action = async () => await subject.UpdateAsync(data, SubjectNameMustBeUniqueChecker);

        //Assert 
        await subject.UpdateAsync(data, SubjectNameMustBeUniqueChecker);

        //Assert
        subject.Should().NotBeNull();
        subject.Name.Should().Be(data.Name);
    }

    [Theory]
    [ClassData(typeof(UpdateNameTestData))]
    public async Task UpgradedUpdatesExamples(string name, string updateName)
    {
        //Arrange
        Mock.Get(SubjectNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var subject = new Subject(new Guid(), name, 99);
        var data = new UpdateSubjectData(updateName, 99);

        //Act
        var action = async () => await subject.UpdateAsync(data, SubjectNameMustBeUniqueChecker);

        //Assert 
        await subject.UpdateAsync(data, SubjectNameMustBeUniqueChecker);

        //Assert
        subject.Should().NotBeNull();
        subject.Name.Should().Be(data.Name);
    }
}