using Sparc.Core;

namespace LawOf100.Features.Features.Entities
{
    public class Habit : Root<string>
    {
        private Habit()
        {
            UserId = string.Empty;
            Id = Guid.NewGuid().ToString();
            HabitName = "Stop Smoking";

        }

        public Habit(string userId, string habitName) : this()
        {
            UserId = userId;
            HabitName = habitName;
        }

        public string UserId { get; private set; }
        public string HabitName { get; set; }
    }
}
