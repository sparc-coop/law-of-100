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

    internal List<Progression> InitializeProgressions(DateTime startDate)
    {
        var progressions = new List<Progression>();
        var targetDate = startDate;
        for (var day = 1; day <= 100; day++)
        {
            progressions.Add(new(day, targetDate));
            targetDate = NextDay(targetDate);
        }

        return progressions;
    }

    internal void RecalculateProgressions(List<Progression> progressions)
    {
        // Adjust future target dates based on actual dates
        var lastTrackedProgression = progressions.Last(x => x.IsTracked);
        if (lastTrackedProgression != null)
        {
            var targetDate = NextDay(lastTrackedProgression.TargetDate);
            for (var day = lastTrackedProgression.Day + 1; day <= 100; day++)
            {
                var progression = progressions.Find(x => x.Day == day);
                progression!.TargetDate = targetDate;
                targetDate = NextDay(targetDate);
            }
        }
        
        // Set missed days
        foreach (var progression in progressions.Where(x => !x.IsTracked))
        {
            if (IsPastFudgeFactor(progression.TargetDate))
                progression.Track(false);

            // TODO: Send Notifications Here
        }
    }

    public double FudgeFactor { get; }

    public bool IsPastFudgeFactor(DateTime day)
    {
        return day.AddHours(RepeatEveryXHours * FudgeFactor) > DateTime.UtcNow;
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
