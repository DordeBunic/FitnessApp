using System.Text.Json.Serialization;

namespace FitnessApp.Models.Api.User;

public class UserSettingsDto
{
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
    [JsonPropertyName("type")]
    public UserSettingsTypeDto? Type { get; set; }
    [JsonPropertyName("value")]
    public int? Value { get; set; }
}
public enum UserSettingsTypeDto
{
    None,
    NumberOfActivity,
    ActivityLength
}