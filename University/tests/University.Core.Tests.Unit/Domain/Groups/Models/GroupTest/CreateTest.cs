using FluentAssertions;
using Moq;
using University.Core.Domain.Groups.Common;
using University.Core.Domain.Groups.Models;
using University.Core.Exceptions;

namespace University.Core.Tests.Unit.Domain.Groups.Models.GroupTest;

public class CreateTest
{
    private IGroupNameMustBeUniqueChecker GroupNameMustBeUniqueChecker { get; }

    public CreateTest()
    {
        GroupNameMustBeUniqueChecker = Mock.Of<IGroupNameMustBeUniqueChecker>();
    }

    [Fact]
    public async Task Should_create_group()
    {
        //Arrange
        Mock.Get(GroupNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var name = "Test";

        //Act
        var gruop = await Group.CreateAsync(name, GroupNameMustBeUniqueChecker);

        //Assert 
        gruop.Should().NotBeNull();
        gruop.Name.Should().Be(name);
    }

    [Fact]
    public async Task When_group_name_is_not_unique_Should_throw_exception()
    {
        //Arrange
        Mock.Get(GroupNameMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var name = "Test";

        //Act
        var action = async () => await Group.CreateAsync(name, GroupNameMustBeUniqueChecker);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(Group.Name));
        failure.ErrorMessage.Should().Be($"Group name: '{name}' must be unique.");
    }
}