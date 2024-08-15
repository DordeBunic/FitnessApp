using System.Text.Json.Serialization;

namespace FitnessApp.Models.Api.Activity;

public class ActivityDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; }

    [JsonPropertyName("activityType")]
    public ActivityTypeDto? ActivityType { get; set; }

}
