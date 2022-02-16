using Sparc.Core;

namespace LawOf100.Features.Habits.Entities
{
    public class TimeLine : Root<string>
    {
        private TimeLine()
        {
            UserId = string.Empty;
            Id = Guid.NewGuid().ToString();
            HabitName = "untitled";
            Day = 1;
            Rating = "";
            Review = "";
        }

        public TimeLine(string userId, string habitname, int day, string rating, string review) :this()
        {
            UserId = userId;
            HabitName = habitname;
            Day = day;
            Rating = rating;
            Review = review;
        }

        public string UserId { get; set; }
        public string HabitName { get; }
        public int Day { get; }
        public string? Rating { get; }
        public string? Review { get; }
        public bool IsDeleted { get; set; }
    }
}
