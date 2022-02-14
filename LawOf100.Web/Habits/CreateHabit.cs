using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits
{
    public record CreateHabitRequest(string HabitName, Recurrence RecurrenceOptions);
    public class CreateHabit : PublicFeature<CreateHabitRequest, string>
    {

        public CreateHabit(IRepository<Habit> habits)
        {
            Habits = habits;
        }

        public IRepository<Habit> Habits { get; }

        public override async Task<string> ExecuteAsync(CreateHabitRequest request)
        {
            var habit = new Habit("userId", request.HabitName);
            await Habits.AddAsync(habit);
            return habit.Id;
        }
    }
}
