using Sparc.Core;

namespace LawOf100.Features.Habits.Entities;

public class Progression : Root<string>
{
    private Progression()
    {
        Id = Guid.NewGuid().ToString();

        //The day the user is on
        Day = 1;

        //States of the user's days tracked
        DayState = "DayPlaceholder";
        CurrentDay = "CurrentDay";
        FutureDay = "FutureDay";
        PassedDay = "PassedDay";
        CompleteDay = "CompleteDay";
        FailedDay = "FailedDay";
    }

    //Thinking about Adding notes, Review, Stars, anything else?

    public Progression(int day) : this()
    {
        Day = day;
        ActualDate = DateTime.UtcNow;
    }

    public Progression(string daystate) : this()
    {
        DayState = daystate;
    }

    public int Day { get; set; }
    public string DayState { get; set; }
    public string CurrentDay { get; set; }
    public string FutureDay { get; set; }
    public string PassedDay { get; set; }
    public string CompleteDay { get; set; }
    public string FailedDay { get; set; }
    public DateTime ActualDate { get; internal set; }
}
