using Sparc.Core;

namespace LawOf100.Features.Habits.Entities;

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

    public void Track(bool isSuccessful, decimal? rating = null, string? review = null)
    {
        ActualDate = DateTime.UtcNow;
        IsSuccessful = isSuccessful;
        Rating = rating;
        Review = review;
    }

    public int Day { get; set; }
    public DateTime TargetDate { get; internal set; }
    public DateTime? ActualDate { get; internal set; }
    public bool? IsSuccessful { get; internal set; }
    public decimal? Rating { get; set; }
    public string? Review { get; set; }
    public bool IsTracked => ActualDate.HasValue;
}
