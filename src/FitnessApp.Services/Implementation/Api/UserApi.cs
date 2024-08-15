using FitnessApp.Models.Api.User;
using FitnessApp.Services.Interfaces.Api;
using FitnessApp.Services.Interfaces.Core;

namespace FitnessApp.Services.Implementation.Api;

public class UserApi : IUserApi
{
    private readonly IApiService _apiService;
    public UserApi(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<UserDto> GetUser(string email)
    {
       return await _apiService.GetRequest<UserDto>($"user?email={email}");
    }
    public async Task Register(UserDto user)
    {
        await _apiService.PostRequest($"user", user);
    }
}
