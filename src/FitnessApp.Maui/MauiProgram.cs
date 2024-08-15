using AutoMapper;
using CommunityToolkit.Maui;
using FitnessApp.Maui.ViewModels.Activity;
using FitnessApp.Maui.ViewModels.Calendar;
using FitnessApp.Maui.ViewModels.Home;
using FitnessApp.Maui.ViewModels.Index;
using FitnessApp.Maui.ViewModels.Login;
using FitnessApp.Maui.ViewModels.User;
using FitnessApp.Maui.Views.Activity;
using FitnessApp.Maui.Views.Calendar;
using FitnessApp.Maui.Views.Home;
using FitnessApp.Maui.Views.Login;
using FitnessApp.Maui.Views.User;
using FitnessApp.Services.Interfaces.Core;
using FitnessApp.Services.Implementation.Core;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using FitnessApp.Services.Interfaces.Service;
using FitnessApp.Services.Implementation.Service;
using FitnessApp.Services.Interfaces.Client;
using FitnessApp.Services.Implementation.Client;
using FitnessApp.Services.Interfaces.Api;
using FitnessApp.Services.Implementation.Api;
using FitnessApp.Services.Helpers.Http;
using FitnessApp.Services.Helpers.AutoMapper;


namespace FitnessApp.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("font-awesome-6-free-regular.otf", "FontAwesomeRegular");
                fonts.AddFont("font-awesome-6-free-solid.otf", "FontAwesomeSolid");
                fonts.AddFont("DarkerGrotesque-VariableFont_wght.ttf", "DarkerGrotesque");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif


        #region ViewModels,Views
        builder.Services.AddSingleton<IndexViewModel>();
        builder.Services.AddViewModel<HomeViewModel, HomeView>();
        builder.Services.AddViewModel<CalendarViewModel, CalendarView>();
        builder.Services.AddViewModel<AddEditActivityViewModel, AddEditActivityView>();
        builder.Services.AddViewModel<UserViewModel, UserView>();
        builder.Services.AddViewModel<LoginViewModel, LoginView>();
        #endregion

        #region AutoMapper
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile(new AutoMapperProfile());
            c.AllowNullCollections = true;
            c.AllowNullDestinationValues = true;
        });

        builder.Services.AddSingleton(mapperConfig.CreateMapper());
        #endregion

        #region Core
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<IToast, Toast>();
        #endregion

        #region Services
        builder.Services.AddSingleton<IActivityService, ActivityService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IPersonalizationService, PersonalizationService>();
        #endregion

        #region Clients
        builder.Services.AddSingleton<IActivityClient, ActivityClient>();
        builder.Services.AddSingleton<IUserClient, UserClient>();
        builder.Services.AddSingleton<IPersonalizationClient, PersonalizationClient>();
        #endregion

        #region Api
        builder.Services.AddSingleton<IActivityApi, ActivityApi>();
        builder.Services.AddSingleton<IUserApi, UserApi>();
        builder.Services.AddSingleton<IPersonalizationApi, PersonalizationApi>();
        #endregion

        #region HttpClient
        builder.Services.AddSingleton(new CustomHttpClient(new CustomHttpHandler()));
        builder.Services.AddHttpClient<IApiService, ApiService>(c => new CustomHttpClient(new CustomHttpHandler()));
        builder.Services.AddSingleton<IApiService, ApiService>();
        #endregion

        return builder.Build();
    }

    private static void AddViewModel<TViewModel, TView>(this IServiceCollection services)
    where TView : ContentPage, new()
    where TViewModel : class
    {
        services.AddTransient<TViewModel>();
        services.AddTransient<TView>(s => new TView() { BindingContext = s.GetRequiredService<TViewModel>() });
    }
}