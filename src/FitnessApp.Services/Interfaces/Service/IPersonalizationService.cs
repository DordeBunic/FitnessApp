using FitnessApp.Models.Domain.User;

namespace FitnessApp.Services.Interfaces.Service;

public interface IPersonalizationService
{
    Task<UserSettings> GetGoal();
    Task SetGoal(UserSettings userSettings);
}
