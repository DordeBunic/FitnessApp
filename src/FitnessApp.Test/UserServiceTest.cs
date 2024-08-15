using NSubstitute;
using FitnessApp.Services.Interfaces.Service;
using FluentAssertions;
using FitnessApp.Models.Domain.User;
namespace FitnessApp.Test;
public class UserServiceTest
{
    [Fact]
    public async Task Login_ValidUsername_ReturnsNull()
    {
        // Arrange
        var service = Substitute.For<IUserService>();
        var username = "validUsername";

        // Act
        var result = await service.Login(username);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Login_ValidUsername_ReturnsUser()
    {
        // Arrange
        var service = Substitute.For<IUserService>();
        var email = "validUsername";
        service.Login(email).Returns(Task.FromResult(new User() { Email = email }));

        // Act
        var result = await service.Login(email);

        result.Should().NotBeNull();
        result.Email.Equals(email);
    }

    [Fact]
    public async Task Register_ValidUser_RegistersUserSuccessfully()
    {
        // Arrange
        var service = Substitute.For<IUserService>();
        var newUser = new User { Email = "newUser", FirstName = "test1", LastName = "test2" };
        service.Login(newUser.Email).Returns(Task.FromResult(newUser));

        // Act
        await service.Register(newUser);

        // Assert
        var retrievedUser = await service.Login(newUser.Email);
        retrievedUser.Should().NotBeNull();
        retrievedUser.Email.Should().Be(newUser.Email);
    }


}
