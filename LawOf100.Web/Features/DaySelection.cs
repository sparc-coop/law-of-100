using LawOf100.Features.Features.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Features
{
    public class DaySelection : PublicFeature<string, List<Habit>>
    {
        public DaySelection(IRepository<Habit> selectdays)
        {
            Selectdays = selectdays;
        }

        public IRepository<Habit> Selectdays { get; }

        public override async Task<List<Habit>> ExecuteAsync(string days)
        {
            var selectdays = await Selectdays.Query
                .Where(x => x.SelectDays == days)
                .ToListAsync();

            return selectdays;

            //var day = new Habit (days);
            //await Selectdays.AddAsync(day);

            //day = Selectdays.Query.FirstOrDefault(x => x.SelectDays == "Monday");

            //return day.Id;
        }
    }
}
