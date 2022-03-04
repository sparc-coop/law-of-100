﻿using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits;


public record ConfimationRequest(string HabitId, int Day, bool IsSuccessful, decimal Rating, string Review);
public class Confirmation : PublicFeature<ConfimationRequest, string>
{
    public Confirmation(IRepository<Habit> habits)
    {
        Habits = habits;
    }

    public IRepository<Habit> Habits { get; }

    public override async Task<string> ExecuteAsync(ConfimationRequest request)
    {
        var habit = await Habits.FindAsync(request.HabitId);
        if (habit == null)
            throw new NotFoundException($"Habit {request.HabitId} not found!");

        habit.HabitConfirmation(request.Day, request.IsSuccessful, request.Rating, request.Review);
        await Habits.UpdateAsync(habit);

        return habit.Id;
    }
}
