using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FitnessApp.Maui.ViewModels.Base;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private bool _isLoading;

    public BaseViewModel()
    {
    }

    [RelayCommand]
    private void NavigateTo(string url)
    {
        Shell.Current.GoToAsync(url);
    }
    protected async Task ShowErrorMessage(string message)
    {
        await Toast.Make(message).Show();
    }

}
