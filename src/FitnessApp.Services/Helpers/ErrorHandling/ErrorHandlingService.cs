using CommunityToolkit.Maui.Alerts;

namespace FitnessApp.Services.Helpers.ErrorHandling;

public class ErrorHandlingService
{
    public ErrorHandlingService()
    {
    }
    public async Task<T> HandleTask<T>(Task<T> task)
    {
        try
        {
            return await task;
        }
        catch (Exception)
        {
            await Toast.Make("Something went wrong").Show();
        }
        return default!;
    }
    public async Task HandleTask(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception)
        {
            await Toast.Make("Something went wrong").Show();
        }
    }
}