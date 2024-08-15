using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Maui.ViewModels.Base;
using FitnessApp.Models.Domain.Activity;
using FitnessApp.Services.Interfaces.Service;
using System.ComponentModel;
using ActivityModel = FitnessApp.Models.Domain.Activity.Activity;

namespace FitnessApp.Maui.ViewModels.Activity;
[QueryProperty(nameof(Activity), nameof(Activity))]
[QueryProperty(nameof(IsEditPage), nameof(IsEditPage))]
public partial class AddEditActivityViewModel : BaseViewModel
{
    [ObservableProperty]
    public ActivityModel _activity;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _desctiption;

    [ObservableProperty]
    private DateOnly _startDate;

    [ObservableProperty]
    private TimeOnly _startTime;

    [ObservableProperty]
    private ActivityType _activityType;

    [ObservableProperty]
    private string _duration;

    [ObservableProperty]
    private bool _isEditPage;


    [ObservableProperty]
    private List<ActivityType> _allActivities;

    private readonly IActivityService _activityService;

    public AddEditActivityViewModel(IActivityService activityService)
    {
        _activityService = activityService;

        Title = "Add/Edit Activity";
        StartDate = DateOnly.FromDateTime(DateTime.Now);
        StartTime = TimeOnly.FromDateTime(DateTime.Now);
        LoadData();
    }
    public async void LoadData()
    {
        AllActivities = await _activityService.GetTypes();
        if (AllActivities != null)
        {
            if (Activity != null)
            {
                ActivityType = AllActivities.FirstOrDefault(x => x.Id == Activity.ActivityType.Id);
            }
            else
                ActivityType = AllActivities.FirstOrDefault()!;
        }
    }
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Activity) && Activity != null)
        {
            Name = Activity.Title;
            Desctiption = Activity.Description;
            StartDate = new DateOnly(Activity.StartTime.Year, Activity.StartTime.Month, Activity.StartTime.Day);
            StartTime = new TimeOnly(Activity.StartTime.Hour, Activity.StartTime.Minute, Activity.StartTime.Second);
            var duration = Activity.EndTime - Activity.StartTime;
            Duration = duration.Minutes.ToString();
        }
        if (e.PropertyName == nameof(IsEditPage) && IsEditPage == false)
        {
            Name = "";
            Desctiption = "";
            Duration = "";
            StartDate = DateOnly.FromDateTime(DateTime.Now);
            StartTime = TimeOnly.FromDateTime(DateTime.Now);
        }

        base.OnPropertyChanged(e);
    }

    [RelayCommand]
    public async Task AddActivity()
    {
        if (await ValidateInput())
        {
            int.TryParse(Duration, out int duration);
            var activity = new ActivityModel()
            {
                Title = Name,
                Description = Desctiption,
                ActivityType = ActivityType,
                StartTime = new DateTime(StartDate, StartTime),
                EndTime = new DateTime(StartDate, StartTime).AddMinutes(duration)

            };
            var result = await _activityService.PostActivity(activity);
            await Shell.Current.GoToAsync("..", true, new()
            {
                {"NewActivity", result }
            });
            Activity = null;
        }
    }

    public async Task<bool> ValidateInput()
    {
        if (string.IsNullOrEmpty(Name))
        {
            await ShowErrorMessage("Please insert name");
            return false;
        }
        if (ActivityType == null)
        {
            await ShowErrorMessage("Please select activity type");
            return false;
        }
        if (string.IsNullOrEmpty(Duration))
        {
            await ShowErrorMessage("Please insert duration");
            return false;
        }
        if (!int.TryParse(Duration, out int duration))
        {
            await ShowErrorMessage("Not valid input for duration");
            return false;
        }
        return true;
    }
    [RelayCommand]
    public async Task UpdateActivity()
    {
        if (await ValidateInput())
        {

            int.TryParse(Duration, out int duration);
            var editedActivity = new ActivityModel()
            {
                Id = Activity.Id,
                Title = Name,
                ActivityType = ActivityType,
                Description = Desctiption,
                StartTime = new DateTime(StartDate, StartTime),
                EndTime = new DateTime(StartDate, StartTime).AddMinutes(duration)
            };

            await _activityService.UpdateActivity(editedActivity);

            await Shell.Current.GoToAsync("..", true, new()
            {
                { "EditedActivity", editedActivity }
            });
            Activity = null;
        }
    }
    [RelayCommand]
    public async Task DeleteActivity()
    {
       var reuslt = await App.Current.MainPage.DisplayActionSheet("Are you sure?", "Yes", "No");
        if (reuslt != "Yes")
            return;
            await _activityService.DeleteActivity(Activity.Id);
            await Shell.Current.GoToAsync("..", true, new()
            {
                {"DeleteActivity", Activity }
            });
            Activity = null;
    }
}
