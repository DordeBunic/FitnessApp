using FitnessApp.Models.Domain.Activity;

namespace FitnessApp.Services.Interfaces.Service;

public interface IActivityService
{
    public Task<List<ActivityType>> GetTypes();
    public Task<List<Activity>> GetActivities(string? searchText = null, 
                                              string? activityTypeId = null,  
                                              DateTime? startDate = null, 
                                              DateTime? endDate = null);
    Task<Activity> PostActivity(Activity activity);
    Task DeleteActivity(string activityId);
    Task UpdateActivity(Activity activity);
}
