using LawOf100.Features.Features.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Features
{
    public class CreateHabit : PublicFeature<string, string>
    {

        public CreateHabit(IRepository<Habit> habits)
        {
            Habits = habits;
        }

        public IRepository<Habit> Habits { get; }

        public override async Task<string> ExecuteAsync(string habitname)
        {
            var habit = new Habit (habitname);
            await Habits.AddAsync(habit);

            // make sure the repo works
            var existingHabit = await Habits.FindAsync(habit.Id);
            existingHabit.HabitName = "New habit name!";
            await Habits.UpdateAsync(habit);

            habit = Habits.Query.FirstOrDefault(x => x.HabitName == "New habit name!");
            return habit.Id;
        }
    }
}
