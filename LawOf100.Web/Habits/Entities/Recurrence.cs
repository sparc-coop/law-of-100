namespace LawOf100.Features.Habits.Entities;

public class Recurrence
{
    private Recurrence()
    {
        RepeatEveryXHours = 24;
        FudgeFactor = 0.5;
    }
    
    public Recurrence(int repeatEveryXHours, double fudgeFactor)
    {
        RepeatEveryXHours = repeatEveryXHours;
        FudgeFactor = fudgeFactor;
    }

    public int RepeatEveryXHours { get; }
    public double FudgeFactor { get; }

    public bool IsPastFudgeFactor(DateTime lastDayTracked)
    {
        return lastDayTracked.AddHours(RepeatEveryXHours * FudgeFactor) > DateTime.UtcNow;
    }

    public DateTime NextDay(DateTime fromDate)
    {
        return fromDate.AddHours(RepeatEveryXHours);
    }

    internal int DaysMissed(DateTime lastProgressDate)
    {
        if (!IsPastFudgeFactor(lastProgressDate))
            return 0;

        var periodsMissed = (DateTime.UtcNow - lastProgressDate).TotalHours / RepeatEveryXHours;

        return (int)Math.Floor(periodsMissed);
    }
}
