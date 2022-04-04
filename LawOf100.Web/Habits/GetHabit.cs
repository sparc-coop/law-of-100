using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record GetHabitRequest(string Username, string HabitId);
public record GetHabitResponse(Habit Habit, List<Timeline> Timeline);
public class GetHabit : PublicFeature<GetHabitRequest, GetHabitResponse>
{
    public GetHabit(IRepository<Habit> habits, IRepository<Account> users)
    {
        Habits = habits;
        Users = users;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Account> Users { get; }

    public override async Task<GetHabitResponse> ExecuteAsync(GetHabitRequest request)
    {
        if (request.HabitId == "test")
        {
            var randomHabit = Habit.Random();
            return new(randomHabit, GetTimeline(randomHabit));
        }

        // Convert username to user id
        var userId = Users.Query.FirstOrDefault(x => x.Nickname == request.Username)?.Id;

        var habit = Habits.Query.FirstOrDefault(x => x.UserId == userId && x.Id == request.HabitId);
        if (habit == null)
            throw new NotFoundException($"Habit {request.HabitId} not found!");

        var timeline = GetTimeline(habit);

        return new(habit, timeline);
    }

    List<Timeline> GetTimeline(Habit currentHabit)
    {
        var habits = new List<Habit> { currentHabit };

        var timelineEntries = new List<Timeline>();
        foreach (var habit in habits)
        {
            foreach (var progression in habit.GetTimeline())
            {
                var entry = new Timeline(habit.Id, habit.HabitName, null, progression.Day, progression.ActualDate!.Value, progression.Rating, progression.Review);
                timelineEntries.Add(entry);
            }
        }

        return timelineEntries.OrderByDescending(x => x.ActualDate).ToList();
    }
}
