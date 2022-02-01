using Sparc.Core;

namespace LawOf100.Features.Features.Entities
{
    public class Habit : Root<string>
    {
        private Habit()
        {
            Id = Guid.NewGuid().ToString();
            HabitName = "Stop Smoking";

        }

        public Habit(string habitName) : this()
        {
            HabitName = habitName;
        }
        public string HabitName { get; set; }
    }
}
