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
        RecurrenceOptions = new Recurrence(0, 0);
        Progressions = new List<Progression>();
    }

    public Habit(string userId, string habitName, int repeatEveryXHours) : this()
    {
        UserId = userId;
        HabitName = habitName;
        RecurrenceOptions = new Recurrence(repeatEveryXHours, 0.5);
    }

    internal void TrackProgress(int day, bool? isSuccessful, decimal rating, string? review)
    {
        throw new NotImplementedException();
    }

    public string UserId { get; private set; }
    public string HabitName { get; private set; }
    public DateTime StartDate { get; private set; }
    public List<Progression> Progressions { get; private set; }

    public Recurrence RecurrenceOptions { get; private set; }
}
