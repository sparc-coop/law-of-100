using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record TrackProgressRequest(string HabitId, int Day, bool IsSuccessful, decimal Rating, string? Review, bool IsPublic);
public class TrackProgress : Feature<TrackProgressRequest, Habit>
{
    public TrackProgress(IRepository<Habit> habits)
    {
        Habits = habits;
    }

    public IRepository<Habit> Habits { get; }

    public override async Task<Habit> ExecuteAsync(TrackProgressRequest request)
    {
        var habit = await Habits.FindAsync(request.HabitId);
        if (habit == null || habit.UserId != User.Id())
            throw new NotFoundException($"Habit {request.HabitId} not found!");

        habit.TrackProgress(request.Day, request.IsSuccessful, request.Rating, request.Review, request.IsPublic);
        await Habits.UpdateAsync(habit);

        return habit;
    }
}
