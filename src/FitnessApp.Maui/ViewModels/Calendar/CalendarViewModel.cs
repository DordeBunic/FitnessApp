using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Maui.ViewModels.Base;
using FitnessApp.Maui.Views.Activity;
using FitnessApp.Models.Domain.User;
using FitnessApp.Services.Interfaces.Service;
using Plugin.Maui.Calendar.Models;
using System.ComponentModel;
using ActivityModel = FitnessApp.Models.Domain.Activity.Activity;

namespace FitnessApp.Maui.ViewModels.Calendar;

[QueryProperty(nameof(DeleteActivity), nameof(DeleteActivity))]
[QueryProperty(nameof(EditedActivity), nameof(EditedActivity))]
public partial class CalendarViewModel : BaseViewModel
{
    [ObservableProperty]
    private EventCollection _events = new();
    [ObservableProperty]
    private List<ActivityModel> _activities;
    [ObservableProperty]
    private Dictionary<DateTime, List<ActivityModel>> _activitiesDictionary;
    [ObservableProperty]
    private UserSettings _userSettings;
    [ObservableProperty]
    ActivityModel _deleteActivity;
    [ObservableProperty]
    ActivityModel _editedActivity;
    [ObservableProperty]
    Color _todaysColor = Colors.Red;

    private int currentPage;
    private int pageSize = 31;
    private DateTime curentDate = DateTime.Now;


    private readonly IActivityService _activityService;
    private readonly IPersonalizationService _personalizationService;
    public CalendarViewModel(IActivityService activityService,
        IPersonalizationService personalizationService)
    {
        _activityService = activityService;
        _personalizationService = personalizationService;
        LoadData();
    }
    private async void LoadData()
    {
        Activities = await _activityService.GetActivities();
        UserSettings = await _personalizationService.GetGoal();
        LoadCalendarData();

    }
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DeleteActivity):
                DeleteActivityFromList(DeleteActivity);
                break;
            case nameof(EditedActivity):
                EditActivityFromList(EditedActivity); 
                break;
            default:
                break;
        }
        base.OnPropertyChanged(e);
    }
    private void LoadCalendarData()
    {
        ActivitiesDictionary = Activities.GroupBy(x => x.StartTime.Date).ToDictionary(x => x.Key, x => x.ToList());

        if(ActivitiesDictionary.ContainsKey(DateTime.Now.Date))
            UpdateTodaysColor(DateTime.Now.Date, ActivitiesDictionary.Where(x=>x.Key == DateTime.Now.Date).FirstOrDefault().Value);
        foreach (var item in ActivitiesDictionary)
        {
            Events.Add(item.Key, new WorkoutCollection<ActivityModel>(item.Value) 
                        { EventIndicatorColor = IsGoalMeet(item.Value) ? Colors.Green : Colors.Red });
        }
        for (int i = 0; i <= pageSize + 6; i++)
        {
            if (!Events.ContainsKey(curentDate.AddDays(i).Date))
                Events.Add(curentDate.AddDays(i).Date, new WorkoutCollection<ActivityModel>() 
                        { EventIndicatorColor = Colors.Red });

            if (!Events.ContainsKey(curentDate.AddDays(i * -1).Date))
                Events.Add(curentDate.AddDays(i * -1).Date, new WorkoutCollection<ActivityModel>() 
                { EventIndicatorColor = Colors.Red });
        }

    }
    private bool IsGoalMeet(List<ActivityModel> activities)
    {
        if (UserSettings.Type == UserSettingsType.NumberOfActivity)
        {
            return UserSettings.Value <= activities.Count;
        }
        else
        {
            return activities.Select(x => (x.EndTime - x.StartTime).TotalMinutes).Sum() >= UserSettings.Value;

        }
    }
    [RelayCommand]
    private void RefreshPage() 
    {
        IsLoading = true;
        LoadData();
        IsLoading = false;
    }

    [RelayCommand]
    private void LoadPrevPage()
    {
        currentPage--;
        for (int i = currentPage * pageSize - pageSize - 6; i < currentPage * pageSize; i++)
        {
            if (!Events.ContainsKey(curentDate.AddDays(i).Date))
                Events.Add(curentDate.AddDays(i).Date, 
                    new WorkoutCollection<ActivityModel>() { EventIndicatorColor = Colors.Red });
        }
    }
    [RelayCommand]
    private void LoadNextPage()
    {
        currentPage++;
        for (int i = currentPage * pageSize; i < currentPage * pageSize + pageSize + 6; i++)
        {
            if (!Events.ContainsKey(curentDate.AddDays(i).Date))
                Events.Add(curentDate.AddDays(i).Date, 
                    new WorkoutCollection<ActivityModel>() { EventIndicatorColor = Colors.Red });
        }
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
    private void DeleteActivityFromList(ActivityModel activity)
    {
        _activityService.DeleteActivity(activity.Id);

        Activities.Remove(activity);
        ActivitiesDictionary[activity.StartTime.Date].Remove(activity);

        var listOfActivities = ActivitiesDictionary.Where(x => x.Key == activity.StartTime.Date).FirstOrDefault();

        Events.Remove(activity.StartTime.Date);
        Events.Add(activity.StartTime.Date, 
            new WorkoutCollection<ActivityModel>(listOfActivities.Value) 
            { EventIndicatorColor = IsGoalMeet(listOfActivities.Value) ? Colors.Green : Colors.Red });
    }
    private void EditActivityFromList(ActivityModel activity)
    {
        Activities[Activities.FindIndex(x=>x.Id == activity.Id)] = activity;

        var activities = ActivitiesDictionary[activity.StartTime.Date];
        activities[activities.FindIndex(x => x.Id == activity.Id)] = activity;
        ActivitiesDictionary[activity.StartTime.Date] = activities;

        Events.Remove(activity.StartTime.Date);
        Events.Add(activity.StartTime.Date,
            new WorkoutCollection<ActivityModel>(activities)
            { EventIndicatorColor = IsGoalMeet(activities) ? Colors.Green : Colors.Red });

    }
    private void UpdateTodaysColor(DateTime date, List<ActivityModel>? activities = null)
    {
        if (activities == null || date != DateTime.Today.Date || !IsGoalMeet(activities))
        {
            TodaysColor = Colors.Red;
            return;
        }
        TodaysColor = Colors.Green;
    }

}
