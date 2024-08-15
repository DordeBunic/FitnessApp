using FitnessApp.Models.Api.User;
namespace FitnessApp.Services.Interfaces.Client;

public interface IPersonalizationClient
{
    Task<UserSettingsDto> GetGoal();
    Task SetGoal(UserSettingsDto userSettings);
}
