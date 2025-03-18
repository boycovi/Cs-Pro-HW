using FluentAssertions;
using Moq;
using University.Core.Domain.Students.Data;
using University.Core.Domain.Students.Models;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Data;
using University.Core.Domain.Teachers.Models;
using University.Core.Exceptions;

namespace University.Core.Tests.Unit.Domain.Teachers.Models.TeachersTests;

public class UpdateTest
{
    private ITeacherFirstNameMustBeInRangeChecker TeacherFirstNameMustBeInRangeChecker { get; }

    public UpdateTest()
    {
        TeacherFirstNameMustBeInRangeChecker = Mock.Of<ITeacherFirstNameMustBeInRangeChecker>();
    }

    [Fact]
    public async Task Should_update_teacher_first_name()
    {
        //Arrange
        Mock.Get(TeacherFirstNameMustBeInRangeChecker)
            .Setup(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var teacher = new Teacher(Guid.NewGuid(), "Test", "Test", "Test");
        var data = new UpdateTeacherData("Billy", "Herrington", "Master");

        //Act
        await teacher.UpdateAsync(data, TeacherFirstNameMustBeInRangeChecker);

        //Assert
        teacher.Should().NotBeNull();
        teacher.FirstName.Should().Be(data.FirstName);
    }

    [Fact]
    public async Task When_a_string_is_out_of_range_Should_throw_exception()
    {
        //Arrange
        Mock.Get(TeacherFirstNameMustBeInRangeChecker)
            .Setup(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var teacher = new Teacher(Guid.NewGuid(), "Test", "Test", "Test");
        var data = new UpdateTeacherData("Billy", "Herrington", "Master");

        //Act
        var action = async () => await teacher.UpdateAsync(data, TeacherFirstNameMustBeInRangeChecker);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(UpdateTeacherData.FirstName));
        failure.ErrorMessage.Should().Be($"{nameof(Teacher)} first name: '{data.FirstName}' must be in the range from 2 to 50.");
    }

    [Theory]
    [InlineData("Tests", "Test")]
    [InlineData("Billy", "Bill")]
    [InlineData("Bochok", "Yarik")]
    public async Task ManyUpdateExample(string name, string updateName)
    {
        //Arrange 
        Mock.Get(TeacherFirstNameMustBeInRangeChecker)
            .Setup(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var teacher = new Teacher(Guid.NewGuid(), name, "Test", "Test");
        var data = new UpdateTeacherData(updateName, "Herrington", "Master");

        //Act
        var action = async () => await teacher.UpdateAsync(data, TeacherFirstNameMustBeInRangeChecker);

        //Assert
        await teacher.UpdateAsync(data, TeacherFirstNameMustBeInRangeChecker);

        //Assert
        teacher.Should().NotBeNull();
        teacher.FirstName.Should().Be(data.FirstName);
    }
}