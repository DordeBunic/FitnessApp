using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Maui.ViewModels.Base;
using FitnessApp.Maui.Views.Activity;
using FitnessApp.Models.Domain.User;
using FitnessApp.Services.Interfaces.Service;
using System;
using System.ComponentModel;
using ActivityModel = FitnessApp.Models.Domain.Activity.Activity;
using ActivityType = FitnessApp.Models.Domain.Activity.ActivityType;

namespace FitnessApp.Maui.ViewModels.Home;

[QueryProperty(nameof(NewActivity), nameof(NewActivity))]
[QueryProperty(nameof(DeleteActivity), nameof(DeleteActivity))]
[QueryProperty(nameof(EditedActivity), nameof(EditedActivity))]
public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<ActivityModel> _allActivities;
    [ObservableProperty]
    ActivityModel _newActivity;
    [ObservableProperty]
    ActivityModel _deleteActivity;
    [ObservableProperty]
    ActivityModel _editedActivity;
    [ObservableProperty]
    private UserSettings _userSettings;
    [ObservableProperty]
    private double? _todaysGoalValue = 0;
    [ObservableProperty]
    private List<ActivityType> _activityTypes;
    [ObservableProperty]
    private ActivityType _activityType;
    [ObservableProperty]
    private string _searchText;
    [ObservableProperty]
    private bool _filterActive = false;
    [ObservableProperty]
    private Color _progressColor;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Today.AddMonths(-1);
    [ObservableProperty]
    private DateTime _endDate = DateTime.Today;

    private readonly IActivityService _activityService;
    private readonly IPersonalizationService _personalizationService;
    public HomeViewModel(IActivityService activityService,
        IPersonalizationService personalizationService)
    {
        _activityService = activityService;
        _personalizationService = personalizationService;
        LoadData();
    }
    protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(NewActivity):
            case nameof(DeleteActivity):
            case nameof(EditedActivity):
                await GetActivities();
                UpdateTodaysGoal();
                break;
            default:
                break;
        }
        base.OnPropertyChanged(e);
    }
    private async void LoadData()
    {
        await GetActivities();
        UpdateTodaysGoal();
        ActivityTypes = await _activityService.GetTypes();
    }

    [RelayCommand]
    private void ReloadData()
    {
        IsLoading = true;
        LoadData();
        IsLoading = false;
        
    }
    [RelayCommand]
    private async Task GoToDetails(ActivityModel activity)
    {
        await Shell.Current.GoToAsync(nameof(AddEditActivityView), true, new()
        {
            {"Activity", activity},
            {"IsEditPage", true }
        });
    }
    [RelayCommand]
    private async Task DeleteActivityFromList(ActivityModel activity)
    {
        await _activityService.DeleteActivity(activity.Id);
        await GetActivities();
        UpdateTodaysGoal();

    }
    [RelayCommand]
    private async Task GoToAdd()
    {
        await Shell.Current.GoToAsync(nameof(AddEditActivityView), true, new()
        {
            {"IsEditPage", false }
        });
    }

    [RelayCommand]
    private async void SetFilter()
    {
        FilterActive = true;
        await GetActivities();
    }
    [RelayCommand]
    private async void ResetFilter()
    {
        FilterActive = false;
        await GetActivities();
    }
    private async Task GetActivities()
    {
        if (FilterActive)
            AllActivities = await _activityService.GetActivities(SearchText, 
                                                                 ActivityType?.Id,
                                                                 StartDate.Date, 
                                                                 EndDate.Date);
        
        else
            AllActivities = await _activityService.GetActivities();

       
    }
    private async void UpdateTodaysGoal()
    {
        UserSettings = await _personalizationService.GetGoal();
        var todaysActivities = AllActivities.Where(x => x.StartTime.Date == DateTime.Today).ToList();

        if (UserSettings.Type == UserSettingsType.NumberOfActivity)
        {
            TodaysGoalValue = todaysActivities.Count / (double)UserSettings.Value;
            
        }
        else
        {
            TodaysGoalValue = todaysActivities.Select(x => (x.EndTime - x.StartTime).TotalMinutes).Sum() / UserSettings.Value;

        }
        if (TodaysGoalValue >= 1)
            ProgressColor = Colors.Green;

        else
            ProgressColor = Colors.Red;
    }
}
