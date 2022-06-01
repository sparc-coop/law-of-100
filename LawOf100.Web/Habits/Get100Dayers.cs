using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record Get100DayersResponse(string HabitName, string Username, DateTime ActualDate, string? Review);
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

        //var habits = await Habits.Query.Where(x => x.Progressions.Any(y => y.Day == 100 && y.IsSuccessful == true)).ToList();

        var habits = await Habits.Query.SelectMany(x => x.Progressions.Where(y => y.Day == 100 && y.IsSuccessful == true)).ToListAsync();


        return null;

        //var dayers = habits
        //    .Select(x => new Get100DayersResponse(
        //        HabitName: x.HabitName,
        //        Username: x.Username,
        //        ActualDate: x.ActualDate,
        //        Review: x.Review
        //        ))
        //    .ToList();

        //return dayers;
    }

    string? UsernameForId(List<Account> users, string id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);
        return user?.Nickname ?? user?.Name;
    }
}