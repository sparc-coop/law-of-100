using Sparc.Core;

namespace LawOf100.Features.Features.Entities
{
    public class Habit : Root<string>
    {
        private Habit()
        {
            Id = Guid.NewGuid().ToString();
            HabitId = 12;
            HabitName = "Stop Smoking";

        }

        public Habit(int habitId, string habitName) : this()
        {
            HabitId = habitId;
            HabitName = habitName;
        }

        public int HabitId { get; set; }
        public string HabitName { get; set; }
    }
}
