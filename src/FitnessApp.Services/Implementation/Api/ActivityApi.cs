using FitnessApp.Models.Api.Activity;
using FitnessApp.Services.Interfaces.Api;
using FitnessApp.Services.Interfaces.Core;

namespace FitnessApp.Services.Implementation.Api;

public class ActivityApi : IActivityApi
{
    private readonly IApiService _apiService;
    public ActivityApi(IApiService apiService)
    {
        _apiService = apiService;
    }
    public Task<List<ActivityDto>> GetActivities(string? searchText, 
                                                 string? activityTypeId, 
                                                 DateTime? startDate, 
                                                 DateTime? endDate)
    {

        string requestQuerry = "?";
        if(!string.IsNullOrEmpty(searchText))
            requestQuerry += "searchText=" + searchText + "&";

        if (!string.IsNullOrEmpty(activityTypeId))
            requestQuerry += "activityTypeId=" + activityTypeId + "&";

        if (startDate != null)
            requestQuerry += "from=" + startDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") + "&";

        if (endDate != null)
            requestQuerry += "to=" + endDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") + "&";

        if (requestQuerry.Length > 1)
            requestQuerry = requestQuerry.Substring(0, requestQuerry.Length - 1);
        else
            requestQuerry = string.Empty;
        var a = _apiService.GetRequest<List<ActivityDto>>($"activity" + requestQuerry);
        return a;
    }

    public Task<List<ActivityTypeDto>> GetTypes()
    {
        return _apiService.GetRequest<List<ActivityTypeDto>>($"activity/types");
    }

    public Task<ActivityDto> PostActivity(ActivityDto activity)
    {
        return _apiService.PostRequest<ActivityDto>($"activity", activity);
    }
    public async Task DeleteActivity(string id)
    {
        await _apiService.DeleteRequest($"activity/?id={id}");
    }

    public async Task UpdateActivity(ActivityDto activity)
    {
       await _apiService.UpdateRequest("activity", activity);
    }
}
