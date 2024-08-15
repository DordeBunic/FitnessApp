using FitnessApp.Models.Api.User;
using FitnessApp.Services.Interfaces.Api;
using FitnessApp.Services.Interfaces.Client;

namespace FitnessApp.Services.Implementation.Client;

public class PersonalizationClient : IPersonalizationClient
{
    private readonly IPersonalizationApi _personalizationApi;
    public PersonalizationClient(IPersonalizationApi personalizationApi)
    {
        _personalizationApi = personalizationApi;
    }
    public async Task<UserSettingsDto> GetGoal()
    {
        return await _personalizationApi.GetGoal();
    }
    public async Task SetGoal(UserSettingsDto userSettings)
    {
       await _personalizationApi.SetGoal(userSettings);
    }
}
