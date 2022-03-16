using Sparc.Core;

namespace LawOf100.Features.Habits.Entities;

public class Habit : Root<string>
{
    private Habit()
    {
        UserId = string.Empty;
        Id = Guid.NewGuid().ToString();
        HabitName = "Stop Smoking";
        StartDate = DateTime.UtcNow;
        Recurrence = new Recurrence(0, 0);
        Progressions = new();
    }

    public Habit(string userId, string habitName, int repeatEveryXHours, double fudgeFactor) : this()
    {
        UserId = userId;
        HabitName = habitName;
        Recurrence = new Recurrence(repeatEveryXHours, fudgeFactor);
        Progressions = Recurrence.InitializeProgressions(StartDate);
    }

    internal void Recalculate()
    {
        Recurrence.RecalculateProgressions(Progressions);
    }

    internal IEnumerable<Progression> GetTimeline()
    {
        return Progressions
            .Where(x => x.IsTracked)
            .OrderByDescending(x => x.ActualDate)
            .ToList();
    }
    
    internal void TrackProgress(int day, bool isSuccessful, decimal? rating = null, string? review = null)
    {
        var progression = Progressions.Find(x => x.Day == day);
        if (progression == null)
            throw new Exception($"Day {day} not found!");

        progression.Track(isSuccessful, rating, review);
        Recalculate();
    }

    public string UserId { get; private set; }
    public string HabitName { get; private set; }
    public DateTime StartDate { get; private set; }
    public bool IsDeleted { get; set; }
    public List<Progression> Progressions { get; private set; }
    public Recurrence Recurrence { get; private set; }
}
