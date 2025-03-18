using FluentAssertions;
using Moq;
using University.Core.Domain.Departments.Common;
using University.Core.Domain.Departments.Models;
using University.Core.Exceptions;

namespace University.Core.Tests.Unit.Domain.Departments.Models.DepartmentTest;

public class CreateTest
{
    private IDepartmentNameMustBeUniqueChecker DepartmentNameMustBeUniqueChecker { get; }

    public CreateTest()
    {
        DepartmentNameMustBeUniqueChecker = Mock.Of<IDepartmentNameMustBeUniqueChecker>();
    }

    [Fact]
    public async Task Should_create_department()
    {
        //Arrange
        Mock.Get(DepartmentNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var name = "Test";

        //Act
        var department = await Department.CreateAsync(name, DepartmentNameMustBeUniqueChecker);

        //Assert 
        department.Should().NotBeNull();
        department.Name.Should().Be(name);
    }

    [Fact]
    public async Task When_department_name_is_not_unique_Should_throw_exception()
    {
        //Arrange
        Mock.Get(DepartmentNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var name = "Test";

        //Act
        var action = async () => await Department.CreateAsync(name, DepartmentNameMustBeUniqueChecker);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(Department.Name));
        failure.ErrorMessage.Should().Be($"Department name: '{name}' must be unique.");
    }
}   
