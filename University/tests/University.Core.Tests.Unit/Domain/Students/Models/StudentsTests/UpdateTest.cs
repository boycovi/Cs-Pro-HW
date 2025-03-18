using FluentAssertions;
using Moq;
using University.Core.Domain.Students.Common;
using University.Core.Domain.Students.Data;
using University.Core.Domain.Students.Models;
using University.Core.Exceptions;

namespace University.Core.Tests.Unit.Domain.Students.Models.StudentsTests;

public class UpdateTest
{
    private IStudentFirstNameMustBeInRangeChecker StudentNameMustBeInRange { get; }

    public UpdateTest()
    {
        StudentNameMustBeInRange = Mock.Of<IStudentFirstNameMustBeInRangeChecker>();
    }

    [Fact]
    public async Task Should_update_student_first_name()
    {
        //Arrange 
        Mock.Get(StudentNameMustBeInRange)
            .Setup(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var student = new Student(new Guid(), "Test", "Test", "Test", Guid.Empty);
        var data = new UpdateStudentData("Billy", "Herrington", "Master");

        //Act
        await student.UpdateAsync(data, StudentNameMustBeInRange);

        //Assert
        student.Should().NotBeNull();
        student.FirstName.Should().Be(data.FirstName);
    }

    [Fact]
    public async Task When_a_string_is_out_of_range_Should_throw_exception()
    {
        //Arrange 
        Mock.Get(StudentNameMustBeInRange)
            .Setup(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var student = new Student(new Guid(), "Test", "Test", "Test", Guid.Empty);
        var data = new UpdateStudentData("Billy", "Herrington", "Master");

        //Act
        var action = async () => await student.UpdateAsync(data, StudentNameMustBeInRange);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(UpdateStudentData.FirstName));
        failure.ErrorMessage.Should().Be($"{nameof(Student)} first name: '{data.FirstName}' must be in the range from 2 to 50.");
    }

    [Theory]
    [InlineData("Tests", "Test")]
    [InlineData("Billy", "Bill")]
    [InlineData("Bochok", "Yarik")]
    public async Task ManyUpdateExample(string name, string updateName)
    {
        //Arrange 
        Mock.Get(StudentNameMustBeInRange)
            .Setup(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var student = new Student(new Guid(), name, "Test", "Test", Guid.Empty);
        var data = new UpdateStudentData(updateName, "Herrington", "Master");

        //Act
        var action = async () => await student.UpdateAsync(data, StudentNameMustBeInRange);

        //Assert
        await student.UpdateAsync(data, StudentNameMustBeInRange);

        //Assert
        student.Should().NotBeNull();
        student.FirstName.Should().Be(data.FirstName);
    }
}