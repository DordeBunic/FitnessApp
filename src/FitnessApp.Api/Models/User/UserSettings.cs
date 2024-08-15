namespace FitensApp.Api.Models.User;

public class UserSettings
{
    public string UserId { get; set; }
    public UserSettingsType Type { get; set; }
    public int Value { get; set; }
}
public enum UserSettingsType
{
    None,
    NumberOfActivity,
    ActivityLength
}
