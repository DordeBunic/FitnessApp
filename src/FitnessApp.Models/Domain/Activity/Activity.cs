namespace FitnessApp.Models.Domain.Activity;

public class Activity
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ActivityType? ActivityType { get; set; }
}
