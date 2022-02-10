using Sparc.Core;
using System.Collections;

namespace LawOf100.Features.Features.Entities
{
    public class Habit : Root<string>
    {
        private Habit()
        {
            UserId = string.Empty;
            Id = Guid.NewGuid().ToString();
            HabitName = "Stop Smoking";
            SelectDays = "string>null;";

        }

        public Habit(string userId, string habitName) : this()
        {
            UserId = userId;
            HabitName = habitName;
        }

        //This would be the list of days e.g. mon - sun
        public Habit(string selectdays) : this()
        {
            SelectDays = selectdays;
        }

        public string UserId { get; set; }
        public string HabitName { get; set; }

        public string SelectDays { get; set; }
    }
}
