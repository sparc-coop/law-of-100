namespace LawOf100.Habits;

public class Progression
{
    private Progression()
    {
        Day = 1;
        TargetDate = DateTime.UtcNow;
    }

    public Progression(int day, DateTime targetDate) : this()
    {
        Day = day;
        TargetDate = targetDate;
    }

    public void Track(bool isSuccessful, decimal? rating = null, string? review = null, bool? isPublic = false, DateTime? actualDate = null)
    {
        ActualDate = actualDate ?? DateTime.UtcNow;
        IsSuccessful = isSuccessful;
        Rating = rating;
        Review = review;
        IsPublic = isPublic;
    }
    internal void Miss()
    {
        IsSuccessful = false;
        ActualDate = null;
    }

    public void AddReaction(string type)
    {
        if (Reactions == null)
            Reactions = new();

        if (!Reactions.Any(x => x.ReactionType == type))
            Reactions.Add(new(type));

        Reactions.First(x => x.ReactionType == type).Count += 1;
    }

    public void RemoveReaction(string type)
    {
        if (Reactions == null || (!Reactions.Any(x => x.ReactionType == type)))
            return;

        Reactions.First(x => x.ReactionType == type).Count -= 1;
    }

    public int Day { get; set; }
    public DateTime TargetDate { get; internal set; }
    public DateTime? ActualDate { get; internal set; }
    public bool? IsSuccessful { get; internal set; }
    public bool? IsPublic { get; internal set; }
    public decimal? Rating { get; set; }
    public string? Review { get; set; }
    public bool IsTracked => IsSuccessful.HasValue;
    public List<ReactionCount>? Reactions { get; set; }
}

public class ReactionCount
{
    private ReactionCount()
    {
        ReactionType = "";
    }

    public ReactionCount(string type) => ReactionType = type;
    
    public string ReactionType { get; set; }
    public int Count { get; set; }
};