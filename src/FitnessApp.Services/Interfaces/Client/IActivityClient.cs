using FitnessApp.Models.Api.Activity;

namespace FitnessApp.Services.Interfaces.Client;

public interface IActivityClient
{
    public Task<List<ActivityTypeDto>> GetTypes();
    public Task<List<ActivityDto>> GetActivities(string? searchText = null, 
                                                 string? activityTypeId = null, 
                                                 DateTime? startDate = null, 
                                                 DateTime? endDate = null);
    Task<ActivityDto> PostActivity(ActivityDto activity);
    Task DeleteActivity(string activityId);
    Task UpdateActivity(ActivityDto activity);
}
