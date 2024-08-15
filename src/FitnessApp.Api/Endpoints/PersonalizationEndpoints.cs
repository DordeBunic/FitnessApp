using FitnessApp.Api;
using Microsoft.AspNetCore.Mvc;
using FitensApp.Api.Models.User;
using Microsoft.AspNetCore.Http.HttpResults;
using FitenssApp.Api.Authentication;

namespace FitensApp.Api.Endpoints;

public static class PersonalizationEndpoints
{
    public static void RegisterPersonalizationEndpoints(this WebApplication app) 
    {
        app.MapGet("v1/user/settings", GetGoal).AddEndpointFilter<AuthenticationFilter>();
        app.MapPost("v1/user/settings", SetGoal).AddEndpointFilter<AuthenticationFilter>();
    }

    static Results<BadRequest, NotFound, Ok<UserSettings>> GetGoal(
    [FromHeader(Name = "x-userId")] string user)
    {
        if (!StaticData.Users.Any(x => x.Id == user))
            return TypedResults.BadRequest();

        var result = StaticData.UserSettings.FirstOrDefault(x => x.UserId == user);
        if (result != null)
            return TypedResults.Ok(new UserSettings() { Type = result.Type, Value = result.Value });
        else
        {
            var newSettings = new UserSettings() 
                { Type = UserSettingsType.NumberOfActivity, UserId = user, Value = 1 };
            StaticData.UserSettings.Add(newSettings);
            return TypedResults.Ok(newSettings);
        }
    }

    static Results<BadRequest, Ok> SetGoal(
            [FromHeader(Name = "x-userId")] string user,
            [FromBody] UserSettings userSettings)
    {
        if (!StaticData.Users.Any(x => x.Id == user))
            return TypedResults.BadRequest();

        if (userSettings == null || userSettings.Value == null || userSettings.Type == null)
            return TypedResults.BadRequest();

        var result = StaticData.UserSettings.FirstOrDefault(x => x.UserId == user);
        if (result == null)
        {
            StaticData.UserSettings.Add(new UserSettings() 
                { UserId = user, Type = userSettings.Type, Value = userSettings.Value });
        }
        else
        {
            StaticData.UserSettings.FirstOrDefault(x => x.UserId == user)!.Value = userSettings.Value;
            StaticData.UserSettings.FirstOrDefault(x => x.UserId == user)!.Type = userSettings.Type;
        }
        return TypedResults.Ok();
    }
}
