using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits
{
    public record Timeline(string HabitName, int Day, DateTime ActualDate, decimal? Rating, string? Review);
    public class GetTimeLine : PublicFeature<string?, List<Timeline>>
    {
        public GetTimeLine(IRepository<Habit> habits)
        {
            Habits = habits;
        }

        public IRepository<Habit> Habits { get; }

        public override async Task<List<Timeline>> ExecuteAsync(string? habitId)
        {
            var habits = habitId == null
                ? await Habits.Query
                .Where(x => x.UserId == User.Id() && x.IsDeleted != true)
                .ToListAsync()
                : new List<Habit> { (await Habits.FindAsync(habitId))! };

            var timelineEntries = new List<Timeline>();
            foreach (var habit in habits)
            {
                foreach (var progression in habit.GetTimeline())
                {
                    var entry = new Timeline(habit.HabitName, progression.Day, progression.ActualDate!.Value, progression.Rating, progression.Review);
                    timelineEntries.Add(entry);
                }
            }

            return timelineEntries.OrderByDescending(x => x.ActualDate).ToList();
        }

        
    }
}
