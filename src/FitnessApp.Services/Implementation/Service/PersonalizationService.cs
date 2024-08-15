using AutoMapper;
using FitnessApp.Models.Api.User;
using FitnessApp.Models.Domain.User;
using FitnessApp.Services.Helpers.ErrorHandling;
using FitnessApp.Services.Interfaces.Client;
using FitnessApp.Services.Interfaces.Service;

namespace FitnessApp.Services.Implementation.Service;

public class PersonalizationService : IPersonalizationService
{
    private readonly IPersonalizationClient _personalizationClient;
    private readonly IMapper _mapper;


    private readonly ErrorHandlingService _errorHandlingService;
    public PersonalizationService(IPersonalizationClient personalizationClient,
        IMapper mapper)
    {
        _personalizationClient = personalizationClient;
        _mapper = mapper;
        _errorHandlingService = new ErrorHandlingService();
    }
    public async Task<UserSettings> GetGoal()
    {
        return _mapper.Map<UserSettings>(
            await _errorHandlingService.HandleTask(_personalizationClient.GetGoal()));
    }

    public async Task SetGoal(UserSettings userSettings)
    {
        var dto = _mapper.Map<UserSettingsDto>(userSettings);
        await _errorHandlingService.HandleTask(_personalizationClient.SetGoal(dto));
    }
}
