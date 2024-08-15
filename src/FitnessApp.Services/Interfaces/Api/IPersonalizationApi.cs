using FitnessApp.Models.Api.User;

namespace FitnessApp.Services.Interfaces.Api;

public interface IPersonalizationApi
{
    Task<UserSettingsDto> GetGoal();
    Task SetGoal(UserSettingsDto goal);
}
