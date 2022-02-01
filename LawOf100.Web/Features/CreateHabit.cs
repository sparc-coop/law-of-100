using LawOf100.Features.Features.Entities;
using Sparc.Features;

namespace LawOf100.Features.Features
{
    public class CreateHabit : PublicFeature<int, string>
    {

        public override async Task<string> ExecuteAsync(int id)
        {
            var habit = new Habit (id, "Stop Smoking");

            return habit.Id;
        }
    }
}
