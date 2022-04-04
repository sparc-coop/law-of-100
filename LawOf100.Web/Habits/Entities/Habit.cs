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

    internal static Habit Random()
    {
        var potentialHours = new[] { 24, 48, 72, 96, 120 };
        var potentialFudgeFactors = new[] { 0.2, 0.5, 0.9 };
        var random = new Random();

        var habit = new Habit("userId", "Test This Grid", potentialHours[random.Next(5)], potentialFudgeFactors[random.Next(3)]);

        var daysTracked = random.Next(0, 101);
        for (var day = 1; day <= daysTracked; day++)
        {
            bool? isSuccess = random.NextDouble() < 0.8 ? true : random.NextDouble() < 0.7 ? null : false;
            int? rating = random.NextDouble() > 0.5 ? random.Next(5) + 1 : null;
            var review = rating != null && random.NextDouble() > 0.5 ? $"This is my review for Day {day}." : null;

            habit.TrackProgress(day, isSuccess, rating, review);
        }

        return habit;
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
    
    internal void TrackProgress(int day, bool? isSuccessful, decimal? rating = null, string? review = null, bool? isPublic = false)
    {
        var progression = Progressions.Find(x => x.Day == day);
        if (progression == null)
            throw new Exception($"Day {day} not found!");

        if (isSuccessful.HasValue)
            progression.Track(isSuccessful.Value, rating, review, isPublic);
        else
            progression.Miss();

        LastTrackedDate = DateTime.UtcNow;
        Recalculate();
    }

    public string UserId { get; private set; }
    public string HabitName { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? LastTrackedDate { get; private set; }
    public bool IsDeleted { get; set; }
    public int? CurrentDay => Progressions.FirstOrDefault(x => !x.IsTracked)?.Day;
    public List<Progression> Progressions { get; private set; }
    public Recurrence Recurrence { get; private set; }
}
