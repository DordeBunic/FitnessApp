using AutoMapper;
using FitnessApp.Models.Domain.User;
using FitnessApp.Services.Helpers.Http;
using FitnessApp.Services.Interfaces.Core;
using FitnessApp.Services.Interfaces.Service;

namespace FitnessApp.Services.Implementation.Core
{
    public class AuthenticationService : IAuthenticationService
    {
        public User? CurrentUser { get; set; }

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthenticationService(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<User?> GetCurrentUser()
        {
            await GetUser();
            UpdateHeaders();
            return CurrentUser;
        }

        public async Task<bool> Login(string username)
        {
            var result = await _userService.Login(username);
            if (result != null)
            {
                CurrentUser = result!;
                SaveUser(System.Text.Json.JsonSerializer.Serialize(CurrentUser));
                UpdateHeaders();
            }
            return result != null;
        }

        public void LogOut()
        {
            CurrentUser = null;
            SecureStorage.RemoveAll();
            UpdateHeaders();
        }

        private async void SaveUser(string user)
        {
           await SecureStorage.SetAsync("user_data", user);
        }
        private async Task GetUser()
        {
            var result = await SecureStorage.GetAsync("user_data");
            if(!string.IsNullOrEmpty(result))
            {
                var a = System.Text.Json.JsonSerializer.Deserialize<User>(result);
                CurrentUser = a;
            }
        }
        private void UpdateHeaders()
        {
            DefaultHeaders.UpdateHeaders(CurrentUser);
        }

}
}
