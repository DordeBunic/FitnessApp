using NSubstitute;
using FitnessApp.Services.Interfaces.Service;
using FluentAssertions;
using FitnessApp.Models.Domain.User;

namespace FitnessApp.Test;
public class PersonalizationServiceTest
{
    [Fact]
    public async Task GetGoal_ReturnsNull()
    {
        // Arrange
        var service = Substitute.For<IPersonalizationService>();

        // Act
        var result = await service.GetGoal();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetGoal_ReturnsUserSettings()
    {
        // Arrange
        var service = Substitute.For<IPersonalizationService>();
        var userSettings = new UserSettings() { Type = UserSettingsType.NumberOfActivity, Value = 1 };
        service.GetGoal().Returns(Task.FromResult(userSettings));

        // Act
        var result = await service.GetGoal();

        // Assert
        result.Should().NotBeNull();
        result.Value.Equals(userSettings.Value);
    }

    [Fact]
    public async Task SetGoal_UserSettings()
    {
        // Arrange
        var service = Substitute.For<IPersonalizationService>();
        var userSettings = new UserSettings() { Type = UserSettingsType.NumberOfActivity, Value = 1 };
        service.GetGoal().Returns(Task.FromResult(userSettings));

        // Act
        await service.SetGoal(userSettings);
        var result = await service.GetGoal();

        // Assert
        result.Should().NotBeNull();
        result.Value.Equals(userSettings.Value);
    }
}
