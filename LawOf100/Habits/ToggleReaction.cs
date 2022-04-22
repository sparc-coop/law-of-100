using Sparc.Core;
using Sparc.Kernel;

namespace LawOf100.Habits;

public record ToggleReactionResponse(List<ReactionCount>? Reactions, List<string> ActiveReactions);
public class ToggleReaction : Feature<Reaction, ToggleReactionResponse>
{
    public ToggleReaction(IRepository<Habit> habits, IRepository<Reaction> reactions)
    {
        Habits = habits;
        Reactions = reactions;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Reaction> Reactions { get; }

    public override async Task<ToggleReactionResponse> ExecuteAsync(Reaction request)
    {
        var habit = await Habits.FindAsync(request.HabitId);
        var reactions = Reactions.Query.Where(x => x.UserId == User.Id() && x.HabitId == request.HabitId && x.Day == request.Day);
        var reaction = reactions.FirstOrDefault(x => x.ReactionType == request.ReactionType);

        var progression = habit!.Progressions.First(x => x.Day == request.Day);
        if (reaction == null)
        {
            reaction = new(User.Id(), request.HabitId, request.Day, request.ReactionType);
            await Reactions.AddAsync(reaction);

            progression.AddReaction(request.ReactionType);
        }
        else if (reaction.ReactionType == request.ReactionType)
        {
            await Reactions.DeleteAsync(reaction);
            progression.RemoveReaction(request.ReactionType);
            reaction = null;
        }
        else
        {
            progression.RemoveReaction(reaction.ReactionType);
            reaction.ReactionType = request.ReactionType;
            reaction.ReactionDate = DateTime.Now;
            await Reactions.UpdateAsync(reaction);

            progression.AddReaction(request.ReactionType);
        }

        await Habits.UpdateAsync(habit);

        return new(progression.Reactions, reactions.Select(x => x.ReactionType).ToList());
    }
}
