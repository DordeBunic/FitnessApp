using AutoMapper;
using FitnessApp.Models.Api.Activity;
using FitnessApp.Models.Api.User;
using FitnessApp.Models.Domain.Activity;
using FitnessApp.Models.Domain.User;

namespace FitnessApp.Services.Helpers.AutoMapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ActivityDto, Activity>();
        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityTypeDto, ActivityType>();
        CreateMap<ActivityType, ActivityTypeDto>();
        CreateMap<UserSettingsDto, UserSettings>();
        CreateMap<UserSettingsTypeDto, UserSettingsType>();
        CreateMap<UserSettings, UserSettingsDto>();
        CreateMap<UserSettingsType, UserSettingsTypeDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();

    }
}
