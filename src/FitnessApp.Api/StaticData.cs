using FitnessApp.Api.Models.User;
using FitnessApp.Api.Models.Activity;
using FitensApp.Api.Models.User;

namespace FitnessApp.Api;

public class StaticData
{
    public static readonly List<User> Users = new List<User>
    {
        new User() { Id = "455b1143-4201-47b7-9641-5e33c5603557", Email = "bunicdorde@gmail.com", FirstName= "Dorde", LastName= "Bunic" },
        new User() { Id = "defdd417-9aab-4bcf-a23d-872b358ffe91", Email = "bunicdejan@gmail.com", FirstName= "Dejan", LastName= "Bunic" },
        new User() { Id = "d66528f5-6745-4706-a269-8aaceaff435a", Email = "peroperic@gmail.com", FirstName= "Pero", LastName= "Peric" },
    };
    public static readonly List<ActivityType> ActivityTypes = new List<ActivityType>
    {
        new ActivityType() { Id = "0d30e54a-3196-4803-b337-caf993e7b43d", Title = "Run" },
        new ActivityType() { Id = "0b42711f-a0d2-44dd-8cf4-e0aa073f9e1b", Title = "Walk" },
        new ActivityType() { Id = "f0b048a7-fa2f-489d-8436-0a842c15d844", Title = "Hike" },
        new ActivityType() { Id = "a4f100f4-81d6-4cc3-b81e-8a102be8703c", Title = "Ride" },
        new ActivityType() { Id = "43338adb-0dc8-4974-b40f-0e090cdbc418", Title = "Swim" },
        new ActivityType() { Id = "59b14565-bbdd-433c-b8e9-fe38ecd7ad91", Title = "Workout" },
        new ActivityType() { Id = "4755e1e0-d445-4bfd-8822-2746d9810a53", Title = "HIIT" },
        new ActivityType() { Id = "ccb52429-2454-4deb-bd34-9e5d61ca9697", Title = "Other" }
    };
    public static readonly List<UserSettings> UserSettings = new List<UserSettings>()
    {
       new UserSettings { UserId ="455b1143-4201-47b7-9641-5e33c5603557", Type = UserSettingsType.ActivityLength, Value = 3 }
    };
    public static readonly List<Activity> Activities = new List<Activity>()
    {
        new Activity() {Id = "d2c8016a-c965-4d30-98c0-53e58c4ccae9", UserId = "455b1143-4201-47b7-9641-5e33c5603557", Title = "Test1", ActivityType = new ActivityType(){ Id = "0d30e54a-3196-4803-b337-caf993e7b43d", Title = "Run" }, Description="Some", StartTime = new DateTime(2024, 8, 5, 19,0,0), EndTime = new DateTime(2024, 8, 5, 19,15,0), CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now },
        new Activity() {Id = "18feb283-cdf4-447b-bd3b-87e31a03791c", UserId = "defdd417-9aab-4bcf-a23d-872b358ffe91", Title = "Test2", ActivityType = new ActivityType(){ Id = "43338adb-0dc8-4974-b40f-0e090cdbc418", Title = "Swim" }, StartTime = new DateTime(2024, 8, 6, 19,4,0), EndTime = new DateTime(2024, 8, 6, 19,15,0), CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now },
        new Activity() {Id = "138a0187-e9a2-4c51-b164-a44df0e80822", UserId = "455b1143-4201-47b7-9641-5e33c5603557", Title = "Test3", ActivityType = new ActivityType(){ Id = "59b14565-bbdd-433c-b8e9-fe38ecd7ad91", Title = "Workout" }, StartTime = new DateTime(2024, 8, 7, 19,0,0), EndTime = new DateTime(2024, 8, 7, 19,4,0), CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now },
        new Activity() {Id = "8fc540d6-09e7-494e-b0f4-b2cadeae7739", UserId = "455b1143-4201-47b7-9641-5e33c5603557", Title = "Test4", ActivityType = new ActivityType(){ Id = "0d30e54a-3196-4803-b337-caf993e7b43d", Title = "Run" }, Description="Some", StartTime = new DateTime(2024, 8, 14, 19,0,0), EndTime = new DateTime(2024, 8, 14, 19,35,0), CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now },
        new Activity() {Id = "8fc540d6-09e7-494e-b0f4-b2cadeae7739", UserId = "455b1143-4201-47b7-9641-5e33c5603557", Title = "Test4", ActivityType = new ActivityType(){ Id = "0d30e54a-3196-4803-b337-caf993e7b43d", Title = "Run" }, Description="Some", StartTime = new DateTime(2024, 8, 14, 19,0,0), EndTime = new DateTime(2024, 8, 14, 19,35,0), CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now },
        new Activity() {Id = "8fc540d6-09e7-494e-b0f4-b2cadeae7739", UserId = "455b1143-4201-47b7-9641-5e33c5603557", Title = "Test4", ActivityType = new ActivityType(){ Id = "0d30e54a-3196-4803-b337-caf993e7b43d", Title = "Run" }, Description="Some", StartTime = new DateTime(2024, 8, 8, 19,0,0), EndTime = new DateTime(2024, 8, 8, 19,35,0), CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now },
    };
}
