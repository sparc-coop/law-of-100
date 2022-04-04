using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits
{
    public record Timeline(string HabitId, string HabitName, string? Username, int Day, DateTime ActualDate, decimal? Rating, string? Review, List<ReactionCount>? Reactions, string? ActiveReaction);
    public class GetTimeLine : PublicFeature<string?, List<Timeline>>
    {
        public GetTimeLine(IRepository<Habit> habits, IRepository<Reaction> reactions)
        {
            Habits = habits;
            Reactions = reactions;
        }

        public IRepository<Habit> Habits { get; }
        public IRepository<Reaction> Reactions { get; }

        public override async Task<List<Timeline>> ExecuteAsync(string? habitId)
        {
            var habits = habitId == null
                ? await Habits.Query
                .Where(x => x.UserId == User.Id() && x.IsDeleted != true)
                .ToListAsync()
                : new List<Habit> { (await Habits.FindAsync(habitId))! };

            var reactions = User.Id() != null
                ? await Reactions.Query.Where(x => x.UserId == User.Id() && x.HabitId == habitId).ToListAsync()
                : new List<Reaction>();

            var timelineEntries = new List<Timeline>();
            foreach (var habit in habits)
            {
                foreach (var progression in habit.GetTimeline())
                {
                    if (User.Id() == null && progression.IsPublic != true)
                        continue;

                    var myReaction = reactions.FirstOrDefault(x => x.Day == progression.Day)?.ReactionType;
                    var entry = new Timeline(habit.Id, 
                        habit.HabitName, 
                        null, 
                        progression.Day, 
                        progression.ActualDate!.Value, 
                        progression.Rating, 
                        progression.Review, 
                        progression.Reactions,
                        myReaction);

                    timelineEntries.Add(entry);
                }
            }

            return timelineEntries.OrderByDescending(x => x.ActualDate).ToList();
        }

        
    }
}
