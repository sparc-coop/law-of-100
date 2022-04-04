using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record Timeline(string HabitId, string HabitName, string? Username, int Day, DateTime ActualDate, decimal? Rating, string? Review);
public record TimelineDay(string DayName, List<Timeline> Entries);
public class GetCommunityTimeline : PublicFeature<List<TimelineDay>>
{
    public GetCommunityTimeline(IRepository<Habit> habits, IRepository<Account> users)
    {
        Habits = habits;
        Users = users;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Account> Users { get; }

    public override async Task<List<TimelineDay>> ExecuteAsync()
    {
        var habits = await Habits.Query
            .OrderByDescending(x => x.LastTrackedDate)
            .Take(100)
            .ToListAsync();

        var userIds = habits.Select(x => x.UserId).Distinct().ToList();
        var users = await Users.Query.Where(x => userIds.Contains(x.UserId)).ToListAsync();

        var timelineEntries = new List<Timeline>();
        foreach (var habit in habits)
        {
            foreach (var progression in habit.GetTimeline().Where(x => x.IsPublic == true))
            {
                var entry = new Timeline(habit.Id, habit.HabitName, UsernameForId(users, habit.UserId), progression.Day, progression.ActualDate!.Value, progression.Rating, progression.Review);
                timelineEntries.Add(entry);
            }
        }

        var today = DateTime.Today;
        var yesterday = DateTime.Today.AddDays(-1);

        var timelines = timelineEntries
            .GroupBy(x => x.ActualDate.Date)
            .OrderByDescending(x => x.Key)
            .Select(x => new TimelineDay(x.Key == today ? "Today" : x.Key == yesterday ? "Yesterday" : x.Key.ToString("MMMM dd, yyyy"),
                    x.OrderByDescending(y => y.ActualDate).ToList()))
            .ToList();

        return timelines;
    }

    string? UsernameForId(List<Account> users, string id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);
        return user?.Nickname ?? user?.Name;
    }
}
