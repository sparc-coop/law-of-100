using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public record CreateHabitRequest(string HabitName, int RepeatEveryXHours, double FudgeFactor);
public class CreateHabit : PublicFeature<CreateHabitRequest, Habit>
{

    public CreateHabit(IRepository<Habit> habits)
    {
        Habits = habits;
    }

    public IRepository<Habit> Habits { get; }

    public override async Task<Habit> ExecuteAsync(CreateHabitRequest request)
    {
        var habit = new Habit("userId", request.HabitName, request.RepeatEveryXHours, request.FudgeFactor);
        await Habits.AddAsync(habit);
        return habit;
    }
}
