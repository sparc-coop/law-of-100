using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record Get100DayersResponse(string HabitName, string? Username, DateTime ActualDate, string? Review);
public class Get100Dayers : PublicFeature<List<Get100DayersResponse>>
{
    public Get100Dayers(IRepository<Habit> habits, IRepository<Account> users)
    {
        Habits = habits;
        Users = users;
    }

    IRepository<Habit> Habits { get; }
    IRepository<Account> Users { get; }

    public override async Task<List<Get100DayersResponse>> ExecuteAsync()
    {
        var habits = await Habits.Query
            .Where(x => x.IsCompleted == true)
            .OrderByDescending(x => x.LastTrackedDate)
            .Take(5)
            .ToListAsync();

        var userIds = habits.Select(x => x.UserId).Distinct().ToList();
        var users = await Users.Query.Where(x => userIds.Contains(x.UserId)).ToListAsync();

        var timelineEntries = new List<Get100DayersResponse>();

        foreach (var habit in habits)
        {
            foreach (var progression in habit.GetTimeline().Where(x => x.IsPublic == true))
            {
                var entry = new Get100DayersResponse(
                    habit.HabitName,
                    UsernameForId(users, habit.UserId),
                    progression.ActualDate!.Value,
                    progression.Review);                                 
                timelineEntries.Add(entry);
            }
        }

        var timelines = timelineEntries
            .OrderByDescending(x => x.ActualDate)
            .Select(x => new Get100DayersResponse(
                HabitName: x.HabitName,
                Username: x.Username,
                ActualDate: x.ActualDate,
                Review: x.Review))                
            .ToList();

        return timelines;
    }

    string? UsernameForId(List<Account> users, string id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);
        return user?.Nickname ?? user?.Name;
    }
}