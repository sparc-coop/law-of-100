﻿using LawOf100.Users;
using Sparc.Core;
using Sparc.Kernel;

namespace LawOf100.Habits;

public record CreateHabitRequest(string HabitName, int RepeatEveryXHours, double FudgeFactor);
public class CreateHabit : Feature<CreateHabitRequest, Habit>
{

    public CreateHabit(IRepository<Habit> habits, IRepository<Users.Account> accounts)
    {
        Habits = habits;
        Accounts = accounts;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Users.Account> Accounts { get; }

    public override async Task<Habit> ExecuteAsync(CreateHabitRequest request)
    {
        var habit = new Habit(User.Id(), request.HabitName, request.RepeatEveryXHours, request.FudgeFactor);
        await Habits.AddAsync(habit);

        await Accounts.ExecuteAsync(User.Id(), u => u.SetCurrentHabit(habit.Id));
        return habit;
    }
}
