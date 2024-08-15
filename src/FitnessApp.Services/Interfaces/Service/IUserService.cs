using FitnessApp.Models.Domain.User;

namespace FitnessApp.Services.Interfaces.Service;

public interface IUserService
{
    Task<User> Login(string username);
    Task Register(User user);
}
