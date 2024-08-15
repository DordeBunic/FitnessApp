using CommunityToolkit.Mvvm.ComponentModel;
using FitnessApp.Maui.ViewModels.Base;
using FitnessApp.Services.Interfaces.Core;

namespace FitnessApp.Maui.ViewModels.Index;

public partial class IndexViewModel : BaseViewModel
{
    [NotifyPropertyChangedFor(nameof(LoggedInNegated))]
    [ObservableProperty]
    private bool _logedIn;
    public bool LoggedInNegated { get => LogedIn; }

    public IndexViewModel(bool logedIn = false)
    { 
        _logedIn = logedIn;
    }
}
