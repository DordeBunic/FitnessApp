using FitnessApp.Models.Api.Activity;
using FitnessApp.Services.Interfaces.Api;
using FitnessApp.Services.Interfaces.Client;

namespace FitnessApp.Services.Implementation.Client;

public class ActivityClient : IActivityClient
{
    private readonly IActivityApi _activityApi;
    public ActivityClient(IActivityApi activityApi)
    {
        _activityApi = activityApi;
    }

    public async Task DeleteActivity(string activityId)
    {
        await _activityApi.DeleteActivity(activityId);
    }

    public Task<List<ActivityDto>> GetActivities(string? searchText, string? activityTypeId, DateTime? startDate, DateTime? endDate)
    {
        return _activityApi.GetActivities(searchText, activityTypeId, startDate, endDate);
    }

    public Task<List<ActivityTypeDto>> GetTypes()
    {
       return _activityApi.GetTypes();
    }

    public Task<ActivityDto> PostActivity(ActivityDto activity)
    {
        return _activityApi.PostActivity(activity);
    }

    public async Task UpdateActivity(ActivityDto activity)
    {
       await _activityApi.UpdateActivity(activity);
    }
}
