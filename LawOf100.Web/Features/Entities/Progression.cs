using Sparc.Core;

namespace LawOf100.Features.Features.Entities
{
    public class Progression : Root<string>
    {
        private Progression()
        {
            Id = Guid.NewGuid().ToString();

            //The day the user is on
            Day = 1;

            //States of the user's days tacked
            CurrentDay = "CurrentDay";
            FutureDay = "FutureDay";
            PassedDay = "PassedDay";
            CompleteDay = "CompleteDay";
            FailedDay = "FailedDay";
        }

        public Progression(int day) : this()
        {
            Day = day;
        }

        public int Day { get; set; }
        public string CurrentDay { get; set; }
        public string FutureDay { get; set; }
        public string PassedDay { get; set; }
        public string CompleteDay { get; set; }
        public string FailedDay { get; set; }
    }
}
