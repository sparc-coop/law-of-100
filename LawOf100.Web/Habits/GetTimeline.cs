using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits
{
    public class GetTimeLine : PublicFeature<List<Habit>>
    {
        public GetTimeLine(IRepository<Habit> habits)
        {
            Habits = habits;
        }

        public IRepository<Habit> Habits { get; }

        public override async Task<List<Habit>> ExecuteAsync()
        {
            var habits = await Habits.Query
                .Where(x => x.UserId == User.Id() && x.IsDeleted != true)
                .ToListAsync();
            foreach (var habit in habits)
            {
                //Get Progressions
                habits.Add();
            }

            return habits;
        }

        
    }
}
