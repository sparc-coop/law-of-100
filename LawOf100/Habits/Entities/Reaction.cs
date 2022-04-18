using Sparc.Core;

namespace LawOf100.Habits;

public class Reaction : Root<string>
{
    public string UserId { get; set; }
    public string HabitId { get; set; }
    public int Day { get; set; }
    public string ReactionType { get; set; }
    public DateTime ReactionDate { get; set; }

    private Reaction()
    {
        Id = Guid.NewGuid().ToString();
        UserId = "";
        HabitId = "";
        ReactionType = "";
    }
    
    public Reaction(string userId, string habitId, int day, string reactionType) : this()
    {
        UserId = userId;
        HabitId = habitId;
        Day = day;
        ReactionType = reactionType;
        ReactionDate = DateTime.UtcNow;
    }
}
