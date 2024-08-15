using FitnessApp.Models.Api.Activity;

namespace FitnessApp.Services.Interfaces.Api
{
    public interface IActivityApi
    {
        Task<List<ActivityTypeDto>> GetTypes();
        Task<List<ActivityDto>> GetActivities(string? searchText = null, 
                                              string? activityTypeId= null, 
                                              DateTime? startDate = null, 
                                              DateTime? endDate = null);
        Task<ActivityDto> PostActivity(ActivityDto activity);
        Task DeleteActivity(string id);
        Task UpdateActivity(ActivityDto activity);
    }
}
