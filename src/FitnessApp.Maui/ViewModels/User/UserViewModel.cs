using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Maui.ViewModels.Base;
using FitnessApp.Models.Domain.User;
using FitnessApp.Services.Interfaces.Core;
using FitnessApp.Services.Interfaces.Service;
using UserModel = FitnessApp.Models.Domain.User.User;


namespace FitnessApp.Maui.ViewModels.User;

public partial class UserViewModel : BaseViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IPersonalizationService _personalizationService;

    [ObservableProperty]
    private string? _userMessage;

    [ObservableProperty]
    private string? _fullName;

    [ObservableProperty]
    private UserSettings _userSettings;

    [ObservableProperty]
    private bool _numberOfActivitySelected;

    [ObservableProperty]
    private string _value;

    private UserModel _user;

    public UserViewModel(IAuthenticationService authenticationService,
        IPersonalizationService personalizationService)
    {
        _authenticationService = authenticationService;
        _personalizationService = personalizationService;
        LoadData();
    }
    public async void LoadData()
    {
        _user = await _authenticationService.GetCurrentUser();
        if (_user == null)
        {
            LogOut();
            return;
        }
            FullName = $"{_user.FirstName} {_user.LastName}";

        UserSettings = await _personalizationService.GetGoal();
        Value = UserSettings.Value.ToString();
        NumberOfActivitySelected = UserSettingsType.NumberOfActivity == UserSettings.Type;
    }
    [RelayCommand]
    public void LogOut()
    {
        _authenticationService.LogOut();
        Application.Current!.MainPage = new FitnessApp.Maui.Views.IndexView(false);
    }
    [RelayCommand]
    public async Task SaveGoal()
    {
        if(!int.TryParse(Value, out int goalValue))
        {
            await ShowErrorMessage("Value must be number");
            return;
        }
        var userSettings = new UserSettings
        {
            Type = NumberOfActivitySelected == true ? UserSettingsType.NumberOfActivity : UserSettingsType.ActivityLength,
            Value = goalValue,
        };
        await _personalizationService.SetGoal(userSettings);
    }

}
