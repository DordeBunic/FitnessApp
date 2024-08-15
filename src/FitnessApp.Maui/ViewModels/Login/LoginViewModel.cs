using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Maui.Views;
using FitnessApp.Maui.ViewModels.Base;
using FitnessApp.Services.Interfaces.Core;
using FitnessApp.Services.Interfaces.Service;

namespace FitnessApp.Maui.ViewModels.Login;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserService _userService;
    private readonly IToast _toast;

    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _firstName;
    [ObservableProperty]
    private string _lastName;
    [ObservableProperty]
    private bool _isRegister;

    public LoginViewModel(IAuthenticationService authenticationService,
        IUserService userService,
        IToast toast)
    {
        _authenticationService = authenticationService;
        _userService = userService;
        _toast = toast;

        LoadData();
    }
    public async void LoadData()
    {
        var user = await _authenticationService.GetCurrentUser();
        if (user != null)
            Application.Current!.MainPage = new IndexView(true);
    }

    [RelayCommand]
    public async Task Login() 
    {
        if (!await _authenticationService.Login(Username))
        {
            await Toast.Make("User not Found").Show();
        }
        else
            Application.Current!.MainPage = new IndexView(true);

    }
    [RelayCommand]
    public async Task Register()
    {
        await _userService.Register(new Models.Domain.User.User
        {
            Email = Username,
            FirstName = FirstName,
            LastName = LastName
        });
        await Login();
    }

}
