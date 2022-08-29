using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Slack;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static LawOf100.Features.Slack.SendMessage;

namespace LawOf100.Features.Habits
{
    public record Get100DayersResponse(DateTime? Date, string? Text, string? User);
    public class Get100Dayers : PublicFeature<List<Get100DayersResponse>>
    {
        public IRepository<Account> Users { get; }
        public IRepository<Habit> Habits { get; }
        public Get100Dayers(IRepository<Account> users, IRepository<Habit> habits)
        {
            Users = users;
            Habits = habits;
        }

        public override async Task<List<Get100DayersResponse>> ExecuteAsync()
        {
            List<Get100DayersResponse> userList = new List<Get100DayersResponse>();
          
            var habits = await Habits.Query
                        .OrderByDescending(x => x.LastTrackedDate)
                        //.Where(x => x.CurrentDay >= 100)
                        //.Take(10)
                        .ToListAsync();

            foreach (var habit in habits)
            {
                if (habit.Progressions.All(x => x.IsPublic == true))
                {
                    var username = Users.Query.Where(x => x.UserId == habit.UserId).First().Nickname;
                    var text = habit.Progressions.Where(x => x.Day == 100).First().Review;
                    var user100 = new Get100DayersResponse(habit.LastTrackedDate, text, username);
                    userList.Add(user100);
                }

            }

            return userList;
        }



    }
}
