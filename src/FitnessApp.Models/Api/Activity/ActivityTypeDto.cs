using System.Text.Json.Serialization;

namespace FitnessApp.Models.Api.Activity;

public class ActivityTypeDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}
