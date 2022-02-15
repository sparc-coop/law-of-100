namespace LawOf100.Features.Habits.Entities;

public class Recurrence
{
    public Recurrence(int repeatEveryXHours, double fudgeFactor)
    {
        RepeatEveryXHours = repeatEveryXHours;
        FudgeFactor = fudgeFactor;
    }

    public int RepeatEveryXHours { get; }
    public double FudgeFactor { get; }

    public bool IsDayMissed(DateTime lastDayTracked)
    {
        return lastDayTracked.AddHours(RepeatEveryXHours * FudgeFactor) > DateTime.UtcNow;
    }
}
