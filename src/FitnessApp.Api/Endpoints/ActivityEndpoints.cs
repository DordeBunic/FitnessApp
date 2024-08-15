using FitnessApp.Api;
using Microsoft.AspNetCore.Mvc;
using FitnessApp.Api.Models.Activity;
using Microsoft.AspNetCore.Http.HttpResults;
using FitenssApp.Api.Authentication;

namespace FitensApp.Api.Endpoints;

public static class ActivityEndpoints
{
    public static void RegisterActivityEndpoints(this WebApplication app)
    {
        app.MapGet("v1/activity", GetActivities).AddEndpointFilter<AuthenticationFilter>();
        app.MapPost("v1/activity", PostActivity).AddEndpointFilter<AuthenticationFilter>();
        app.MapPatch("v1/activity", UpdateActivity).AddEndpointFilter<AuthenticationFilter>();
        app.MapDelete("v1/activity", DeleteActivity).AddEndpointFilter<AuthenticationFilter>();

        app.MapGet("v1/activity/types", GetActivityTypes);
    }
    static Results<BadRequest, Ok<List<Activity>>> GetActivities(
            [FromHeader(Name = "x-userId")] string userId,
            string? searchText,
            string? activityTypeId,
            DateTime? from,
            DateTime? to)
    {
        if (!StaticData.Users.Any(x => x.Id == userId))
            return TypedResults.BadRequest();

        if (!from.HasValue)
            from = DateTime.MinValue;

        if (!to.HasValue)
            to = DateTime.MaxValue;

        var result = StaticData.Activities.Where(x => x.UserId == userId && 
                                                 x.StartTime > from && 
                                                 x.EndTime < to).ToList();
        if (result == null || result.Count == 0)
            return TypedResults.Ok(new List<Activity>());

        if (!string.IsNullOrEmpty(activityTypeId))
            result = result.Where(x=> x.ActivityType!= null &&  
                                  x.ActivityType.Id == activityTypeId).ToList();

        if (result == null || result.Count == 0)
            return TypedResults.Ok(new List<Activity>());

        if (!string.IsNullOrEmpty(searchText))
            result = result.Where(x => x.Title.ToLower().Contains(searchText.ToLower()) || 
                                       !string.IsNullOrEmpty(x.Description) &&
                                       x.Description.ToLower().Contains(searchText.ToLower())).ToList();

        if (result == null || result.Count == 0)
            return TypedResults.Ok(new List<Activity>());
        return TypedResults.Ok(result.OrderByDescending(x=>x.StartTime).ToList());
    }
    static Results<BadRequest, Ok<Activity>> PostActivity(
            [FromHeader(Name = "x-userId")] string userId,
            [FromBody] Activity activity)
    {
        if (!StaticData.Users.Any(x => x.Id == userId))
            return TypedResults.BadRequest();
        var activityType = StaticData.ActivityTypes.
            Where(x => x.Id == activity?.ActivityType?.Id).FirstOrDefault();
        
        if (activityType == null ||
        activity?.StartTime == DateTime.MinValue ||
        activity?.EndTime == DateTime.MinValue)
            return TypedResults.BadRequest();

        activity.Id = Guid.NewGuid().ToString();
        activity.UserId = userId;
        activity.CreatedTime = DateTime.Now;
        activity.ModifiedTime = activity.CreatedTime;

        StaticData.Activities.Add(activity);

        return TypedResults.Ok(activity);
    }
    static Results<BadRequest, Ok> UpdateActivity(
        [FromHeader(Name = "x-userId")] string userId,
        [FromBody] Activity activity)
    {
        if (!StaticData.Users.Any(x => x.Id == userId))
            return TypedResults.BadRequest();

        if (!StaticData.ActivityTypes.Any(x => x.Id == activity.ActivityType.Id))
            return  TypedResults.BadRequest();

        var oldActivity = StaticData.Activities.FirstOrDefault(x => x.Id == activity.Id);
        if (oldActivity == null)
            return TypedResults.BadRequest();

        activity.ModifiedTime = DateTime.Now;
        activity.UserId = oldActivity.UserId;
        var index = StaticData.Activities.IndexOf(oldActivity);
        StaticData.Activities[index] = activity;

        return TypedResults.Ok();
    }
    static Results<BadRequest, NotFound, NoContent> DeleteActivity(
            [FromHeader(Name = "x-userId")] string userId,
            string id)
    {
        if (!StaticData.Users.Any(x => x.Id == userId))
            return TypedResults.BadRequest();

        if (!StaticData.Activities.Any(x => x.Id == id))
            return TypedResults.NotFound();

        var activity = StaticData.Activities.Where(x => x.Id == id && 
                                                   x.UserId == userId).ToList();
        var activityIndex = activity.FindIndex(x => x.Id == id);
        StaticData.Activities.Remove(activity[activityIndex]);
        return TypedResults.NoContent();
    }
    static Results<BadRequest, Ok<List<ActivityType>>> GetActivityTypes()
    {
        return TypedResults.Ok(StaticData.ActivityTypes);
    }
}
