using LawOf100.Features.Features.Entities;
using Sparc.Features;

namespace LawOf100.Features.Features
{
    public class CreateHabit : PublicFeature<string, string>
    {

        public override async Task<string> ExecuteAsync(string habitname)
        {
            var habit = new Habit (habitname);

            return habit.Id;
        }
    }
}
