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
        Progressions = new List<Progression>();
    }

    public Habit(string userId, string habitName, int repeatEveryXHours) : this()
    {
        UserId = userId;
        HabitName = habitName;
        Recurrence = new Recurrence(repeatEveryXHours, 0.5);
    }

    internal void CheckIfOverdue()
    {
        var lastProgressDate = Progressions.Any()
            ? Progressions.Max(x => x.ActualDate)
            : StartDate;

        var daysMissed = Recurrence.DaysMissed(lastProgressDate);
        if (daysMissed > 0)
        {
            var lastProgressDay = Progressions.Max(x => x.Day);
            for (var missedDay = lastProgressDay + 1; missedDay <= lastProgressDay + daysMissed; missedDay++)
                TrackProgress(missedDay, false);
        }
    }
    
    internal void TrackProgress(int day, bool? isSuccessful, decimal? rating = null, string? review = null)
    {
        // TODO: Add code to actually add the progression to the habit
    }

    public string UserId { get; private set; }
    public string HabitName { get; private set; }
    public DateTime StartDate { get; private set; }
    public List<Progression> Progressions { get; private set; }

    public Recurrence Recurrence { get; private set; }
}
