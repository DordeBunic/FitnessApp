using FitnessApp.Maui.ViewModels.Index;
using FitnessApp.Maui.Views.Activity;
using FitnessApp.Maui.Views.Home;
using FitnessApp.Services.Interfaces.Core;

namespace FitnessApp.Maui.Views;

public partial class IndexView : Shell
{
    public IndexView(bool loggedIn = false)
    {
        InitializeComponent();
        BindingContext = new IndexViewModel(loggedIn);


        Routing.RegisterRoute(nameof(AddEditActivityView), typeof(AddEditActivityView));
    }
}
