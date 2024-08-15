using FitnessApp.Models.Api.User;

namespace FitnessApp.Services.Interfaces.Client;

public interface IUserClient
{
    public Task<UserDto> LogIn(string userName);
    public Task Register(UserDto user);
}
