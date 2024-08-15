using FitnessApp.Api;
using FitnessApp.Api.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using FitenssApp.Api.Authentication;

namespace FitensApp.Api.Endpoints;

public static class UserEndpoints
{
    
    public static void RegisterUserEndpoints(this WebApplication app)
    {
        app.MapGet("v1/user", GetUser).AddEndpointFilter<AuthenticationFilter>();
        app.MapPost("v1/user", PostUser).AddEndpointFilter<AuthenticationFilter>();
    }

    static Results<BadRequest, NotFound, Ok<User>> GetUser(string email)
    {
        var data = StaticData.Users;
        var user = data.FirstOrDefault(x => x.Email == email);
        if (user == null)
            return TypedResults.NotFound();
        else
            return TypedResults.Ok(user);
    }

    static Results<BadRequest, Ok> PostUser(
            [FromBody] User newUser)
    {
        if (newUser == null || string.IsNullOrEmpty(newUser.FirstName)
        || string.IsNullOrEmpty(newUser.LastName)
        || string.IsNullOrEmpty(newUser.Email))
            return TypedResults.BadRequest();

        newUser.Id = Guid.NewGuid().ToString();
        StaticData.Users.Add(newUser);
        return TypedResults.Ok();
    }
}
