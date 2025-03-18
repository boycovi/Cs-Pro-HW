using FluentAssertions;
using Moq;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Faculties.Models;
using University.Core.Exceptions;

namespace University.Core.Tests.Unit.Domain.Faculties.Models.FacultyTest;

public class CreateTest
{
    private IFacultyNameMustBeUniqueChecker FacultyNameMustBeUniqueChecker { get; }

    public CreateTest()
    {
        FacultyNameMustBeUniqueChecker = Mock.Of<IFacultyNameMustBeUniqueChecker>();
    }

    [Fact]
    public async Task Should_create_faculty()
    {
        //Arrange
        Mock.Get(FacultyNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var name = "Test";

        //Act
        var faculty = await Faculty.CreateAsync(name, FacultyNameMustBeUniqueChecker);

        //Assert 
        faculty.Should().NotBeNull();
        faculty.Name.Should().Be(name);
    }

    [Fact]
    public async Task When_faculty_name_is_not_unique_Should_throw_exception()
    {
        //Arrange
        Mock.Get(FacultyNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var name = "Test";

        //Act
        var action = async () => await Faculty.CreateAsync(name, FacultyNameMustBeUniqueChecker);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(Faculty.Name));
        failure.ErrorMessage.Should().Be($"Faculty name: '{name}' must be unique.");
    }
}