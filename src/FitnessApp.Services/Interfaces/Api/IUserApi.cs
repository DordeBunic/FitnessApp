using FitnessApp.Models.Api.User;

namespace FitnessApp.Services.Interfaces.Api;

public interface IUserApi
{
    public Task<UserDto> GetUser(string userName);
    public Task Register(UserDto user);
}
