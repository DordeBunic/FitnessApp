using FitnessApp.Models.Domain.Activity;
using NSubstitute;
using FitnessApp.Services.Interfaces.Service;
using FluentAssertions;

namespace FitnessApp.Test;

public class ActivityServiceTest
{
    [Fact]
    public async Task GetActivities_ReturnsNull()
    {
        // Arrange
        var activitySevice = Substitute.For<IActivityService>();

        // Act
        var retult = await activitySevice.GetActivities();

        // Assert
        retult.Should().BeNull();
    }

    [Fact]
    public async Task GetActivities_ReturnsList()
    {
        // Arrange
        var activitySevice = Substitute.For<IActivityService>();
        activitySevice.GetActivities().Returns(Task.FromResult(new List<Activity>()));
        // Act
        var retult = await activitySevice.GetActivities();

        // Assert
        retult.Should().NotBeNull();
        retult.Count.Should().Be(0);

    }

    [Fact]
    public async Task GetActivities_Filter_ReturnsList()
    {
        // Arrange
        var activitySevice = Substitute.For<IActivityService>();
        activitySevice.GetActivities("test").Returns(Task.FromResult(new List<Activity>() { new Activity() { ActivityType = new ActivityType { Id = "1", Title = "Run" }, Title = "test", Description = "testt" } }));
        
        // Act
        var result = await activitySevice.GetActivities("test");

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().BeGreaterThan(0);
        result.Count.Equals(
            result.Where(x=>!string.IsNullOrEmpty(x.Title) && x.Title.Contains("test") || 
            !string.IsNullOrEmpty(x.Description) && x.Description.Contains("test")).ToList().Count.Should().Be(1));
    }

    [Fact]
    public async Task GetTypes_ReturnsNull()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();

        // Act
        var result = await service.GetTypes();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTypes_ReturnsEmptyList()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();
        service.GetTypes().Returns(new List<ActivityType>());
        
        // Act
        var result = await service.GetTypes();

        // Assert
        result.Should().NotBeNull();
        result.Count.Equals(0);
    }

    [Fact]
    public async Task GetTypes_ReturnsList()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();
        service.GetTypes().Returns(new List<ActivityType>() { new ActivityType() { Id = "1", Title = "test" }, new ActivityType() { Id = "2", Title = "test2" } });
        
        // Act
        var result = await service.GetTypes();

        // Assert
        result.Should().NotBeNull();
        result.Count.Equals(2);
    }

    [Fact]
    public async Task PostActivity_ReturnsNull()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();

        // Act
        var result = await service.PostActivity((new Activity() { }));

        // Assert
        result.Should().BeNull();;
    }

    [Fact]
    public async Task PostActivity_ReturnsNewActivity()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();
        var activity = new Activity();
        service.PostActivity(activity).Returns(Task.FromResult(activity));

        // Act
        var result = await service.PostActivity(activity);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task PostActivity_ReturnsActivity()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();
        var activity = new Activity() { Title = "test1" };
        service.PostActivity(activity).Returns(Task.FromResult(activity));

        // Act
        var result = await service.PostActivity(activity);

        // Assert
        result.Should().NotBeNull();
        result.Title.Equals(activity.Title);
    }

    [Fact]
    public async Task DeleteActivity_ValidId_DeletesActivity()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();
        var activityId = "someValidId"; // Example ID

        // Act
        await service.DeleteActivity(activityId);
    }

    [Fact]
    public async Task UpdateActivity_ValidActivity_UpdatesExistingActivity()
    {
        // Arrange
        var service = Substitute.For<IActivityService>();
        var activityToUpdate = new Activity() { Title = "Title" };
        var activityUpdatedValue = new List<Activity>() { new Activity() { Title = "New Title" } };
        service.GetActivities().Returns(Task.FromResult(activityUpdatedValue));


        // Act
        await service.UpdateActivity(activityUpdatedValue.FirstOrDefault());
        var result = await service.GetActivities();

        // Assert
        result.FirstOrDefault().Should().NotBe(activityToUpdate);


    }
}
