using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;

public class GetHabit : PublicFeature<string, Habit>
{

    public GetHabit(IRepository<Habit> habits)
    {
        Habits = habits;
    }

    public IRepository<Habit> Habits { get; }

    public override async Task<Habit> ExecuteAsync(string id)
    {
        var habit = await Habits.FindAsync(id);
        if (habit == null)
            throw new NotFoundException($"Habit {id} not found!");

        return habit;
    }
}
