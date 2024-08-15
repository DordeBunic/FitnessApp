using AutoMapper;
using CommunityToolkit.Maui.Core;
using FitnessApp.Models.Api.User;
using FitnessApp.Models.Domain.User;
using FitnessApp.Services.Helpers.ErrorHandling;
using FitnessApp.Services.Interfaces.Client;
using FitnessApp.Services.Interfaces.Service;

namespace FitnessApp.Services.Implementation.Service;

public class UserService : IUserService
{
    protected readonly IUserClient _userClient;
    protected readonly IToast _toast;
    protected readonly IMapper _mapper;

    private readonly ErrorHandlingService _errorHandlingService;
    public UserService(IUserClient userClient,
        IToast toast,
        IMapper mapper)
    {
        _userClient = userClient;
        _toast = toast;
        _mapper = mapper;

        _errorHandlingService = new ErrorHandlingService();
    }

    public async Task<User> Login(string username)
    {
        return _mapper.Map<User>(
            await _errorHandlingService.HandleTask(_userClient.LogIn(username)));
    }

    public async Task Register(User user)
    {
        var dto = _mapper.Map<UserDto>(user);
        await _errorHandlingService.HandleTask(_userClient.Register(dto));
    }
}
