using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;
using Sparc.Notifications.Azure;

namespace LawOf100.Features.Habits;

public record TrackProgressRequest(string HabitId, int Day, bool IsSuccessful, decimal Rating, string? Review, bool IsPublic);
public class TrackProgress : Feature<TrackProgressRequest, Habit>
{
    public TrackProgress(IRepository<Habit> habits, AzureNotificationService notifications)
    {
        Habits = habits;
        Notifications = notifications;
    }

    public IRepository<Habit> Habits { get; }
    public AzureNotificationService Notifications { get; }

    public override async Task<Habit> ExecuteAsync(TrackProgressRequest request)
    {
        var habit = await Habits.FindAsync(request.HabitId);
        if (habit == null || habit.UserId != User.Id())
            throw new NotFoundException($"Habit {request.HabitId} not found!");

        habit.TrackProgress(request.Day, request.IsSuccessful, request.Rating, request.Review, request.IsPublic);
        await Habits.UpdateAsync(habit);

        Message message = new($"Time to track Day {request.Day}!",
            "Click here to track and share your day's progress with Law of 100.");
        message.ClickAction = $"/habits/{request.HabitId}";

        await Notifications.ScheduleAsync(User.Id(), message, habit.Progressions.Find(x => x.Day == request.Day).ActualDate.Value.AddSeconds(10));

        return habit;
    }
}
