using Sparc.Core;
using Sparc.Kernel;
using Sparc.Notifications.Azure;

namespace LawOf100.Habits;

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
        var habit = Habits.Query.FirstOrDefault(x => x.UserId == User.Id() && x.Id == request.HabitId);
        if (habit == null)
            throw new NotFoundException($"Habit {request.HabitId} not found!");

        habit.TrackProgress(request.Day, request.IsSuccessful, request.Rating, request.Review, request.IsPublic);
        await Habits.UpdateAsync(habit);

        Message message = new($"Time to track Day {habit.CurrentDay}!",
            "Click here to track and share your day's progress with Law of 100.")
        {
            ClickAction = $"/habit"
        };

        var nextEditableDate = habit.NextEditableDate();
        if (nextEditableDate != null)
            await Notifications.ScheduleAsync(User.Id(), message, nextEditableDate.Value);

        return habit;
    }
}
