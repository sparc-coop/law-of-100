using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record CreateHabitRequest(string HabitName, int RepeatEveryXHours, double FudgeFactor);
public class CreateHabit : Feature<CreateHabitRequest, Habit>
{

    public CreateHabit(IRepository<Habit> habits, IRepository<Account> accounts)
    {
        Habits = habits;
        Accounts = accounts;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Account> Accounts { get; }

    public override async Task<Habit> ExecuteAsync(CreateHabitRequest request)
    {
        var habit = new Habit(User.Id(), request.HabitName, request.RepeatEveryXHours, request.FudgeFactor);
        await Habits.AddAsync(habit);

        await Accounts.ExecuteAsync(User.Id(), u => u.SetCurrentHabit(habit.Id));
        return habit;
    }
}
