using Sparc.Core;

namespace LawOf100.Features.Habits.Entities;

public class Habit : Root<string>
{
    private Habit()
    {
        UserId = string.Empty;
        Id = Guid.NewGuid().ToString();
        HabitName = "Stop Smoking";
        RecurrenceOptions = new Recurrence();
        Progressions = new List<Progression>();
    }

    public Habit(string userId, string habitName) : this()
    {
        UserId = userId;
        HabitName = habitName;
    }

    internal void TrackProgress(int day, bool? isSuccessful, decimal rating, string? review)
    {
        throw new NotImplementedException();
    }

    public string UserId { get; set; }
    public string HabitName { get; set; }
    public List<Progression> Progressions { get; set; }

    public Recurrence RecurrenceOptions { get; set; }
}
