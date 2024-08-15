namespace FitnessApp.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new Maui.Views.IndexView(false);
    }
}
