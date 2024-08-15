using FitnessApp.Models.Api.User;
using FitnessApp.Services.Interfaces.Api;
using FitnessApp.Services.Interfaces.Client;

namespace FitnessApp.Services.Implementation.Client;

public class UserClient : IUserClient
{
    private readonly IUserApi _userApi;

    public UserClient(IUserApi userApi)
    {
        _userApi = userApi;
    }

    public Task<UserDto> LogIn(string userName)
    {
        return _userApi.GetUser(userName);
    }

    public Task Register(UserDto user)
    {
        return _userApi.Register(user);
    }
}
