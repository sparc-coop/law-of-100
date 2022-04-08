using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record GetHabitRequest(string Username, string HabitId);
public record GetHabitResponse(Habit Habit, List<Timeline> Timeline, bool IsEditable, DateTime? NextEditableDate);
public class GetHabit : PublicFeature<GetHabitRequest, GetHabitResponse>
{
    public GetHabit(IRepository<Habit> habits, IRepository<Reaction> reactions, IRepository<Account> users)
    {
        Habits = habits;
        Reactions = reactions;
        Users = users;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Reaction> Reactions { get; }
    public IRepository<Account> Users { get; }

    public override async Task<GetHabitResponse> ExecuteAsync(GetHabitRequest request)
    {
        if (request.HabitId == "test")
        {
            var randomHabit = Habit.Random();
            return new(randomHabit, await GetTimelineAsync(randomHabit), true, randomHabit.NextEditableDate());
        }

        // Convert username to user id
        var userId = Users.Query.FirstOrDefault(x => x.Nickname == request.Username)?.Id;

        var habit = Habits.Query.FirstOrDefault(x => x.UserId == userId && x.Id == request.HabitId);
        if (habit == null)
            throw new NotFoundException($"Habit {request.HabitId} not found!");

        var timeline = await GetTimelineAsync(habit);
        return new(habit, timeline, habit.UserId == User.Id(), habit.NextEditableDate());
    }

    async Task<List<Timeline>> GetTimelineAsync(Habit currentHabit)
    {
        var habits = new List<Habit> { currentHabit };

        var reactions = User.Id() != null
              ? await Reactions.Query.Where(x => x.UserId == User.Id() && x.HabitId == currentHabit.Id).ToListAsync()
              : new List<Reaction>();

        var timelineEntries = new List<Timeline>();
        foreach (var habit in habits)
        {
            foreach (var progression in habit.GetTimeline())
            {
                if (User.Id() == null && progression.IsPublic != true)
                    continue;

                var myReactions = reactions.Where(x => x.Day == progression.Day).Select(x => x.ReactionType).ToList();
                var entry = new Timeline(habit.Id,
                    habit.HabitName,
                    null,
                    progression.Day,
                    progression.ActualDate!.Value,
                    progression.Rating,
                    progression.Review,
                    progression.Reactions,
                    myReactions);

                timelineEntries.Add(entry);
            }
        }

        return timelineEntries.OrderByDescending(x => x.ActualDate).ToList();
    }
}
