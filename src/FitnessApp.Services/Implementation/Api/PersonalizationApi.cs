using FitnessApp.Models.Api.User;
using FitnessApp.Services.Interfaces.Api;
using FitnessApp.Services.Interfaces.Core;

namespace FitnessApp.Services.Implementation.Api;

public class PersonalizationApi : IPersonalizationApi
{
    protected readonly IApiService _apiService;
    public PersonalizationApi(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<UserSettingsDto> GetGoal()
    {
        return await _apiService.GetRequest<UserSettingsDto>($"user/settings");
    }

    public async Task SetGoal(UserSettingsDto goal)
    {
        await _apiService.PostRequest($"user/settings", goal);
    }
}
