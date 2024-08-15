using FitnessApp.Models.Domain.User;

namespace FitnessApp.Services.Interfaces.Core;

public interface IAuthenticationService
{
    Task<bool> Login(string username);
    Task<User> GetCurrentUser();
    void LogOut();
}
