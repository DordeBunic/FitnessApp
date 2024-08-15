using AutoMapper;
using CommunityToolkit.Maui.Core;
using FitnessApp.Models.Api.Activity;
using FitnessApp.Models.Domain.Activity;
using FitnessApp.Services.Helpers.ErrorHandling;
using FitnessApp.Services.Interfaces.Client;
using FitnessApp.Services.Interfaces.Service;

namespace FitnessApp.Services.Implementation.Service;

public class ActivityService : IActivityService
{
    private readonly IActivityClient _activityClient;
    private readonly IToast _toast;
    private readonly IMapper _mapper;

    private readonly ErrorHandlingService _errorHandlingService;

    public ActivityService(IActivityClient activityClient,
        IToast toast,
        IMapper mapper)
    {
        _activityClient = activityClient;
        _toast = toast;
        _mapper = mapper;
        _errorHandlingService = new ErrorHandlingService();
    }

    public async Task<List<ActivityType>> GetTypes()
    {
        return _mapper.Map<List<ActivityType>>(
            await _errorHandlingService.HandleTask(_activityClient.GetTypes()));
    }

    public async Task<List<Activity>> GetActivities(string? searchText, 
                                                    string activityTypeId, 
                                                    DateTime? startDate, 
                                                    DateTime? endDate)
    {
        return _mapper.Map<List<Activity>>(
            await _errorHandlingService.HandleTask(
                _activityClient.GetActivities(searchText, activityTypeId, startDate, endDate)));
    }

    public async Task<Activity> PostActivity(Activity activity)
    {
        var dto = _mapper.Map<ActivityDto>(activity);
        return _mapper.Map<Activity>(
            await _errorHandlingService.HandleTask(_activityClient.PostActivity(dto)));
    }

    public async Task DeleteActivity(string activityId)
    {
        await _errorHandlingService.HandleTask(_activityClient.DeleteActivity(activityId));
    }

    public async Task UpdateActivity(Activity activity)
    {
        var dto = _mapper.Map<ActivityDto>(activity);
        await _errorHandlingService.HandleTask(_activityClient.UpdateActivity(dto));
    }
}

