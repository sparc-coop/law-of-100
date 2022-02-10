using LawOf100.Features.Features.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Features
{
    public class DayState : PublicFeature<string, string>
    {
        
        public DayState(IRepository<Progression> daystates)
        {
            DayStates = daystates;
        }

        public IRepository<Progression> DayStates { get; }

        public override async Task<string> ExecuteAsync(string currentstate)
        {
            var daystate = new Progression(currentstate);
            await DayStates.AddAsync(daystate);

            daystate = DayStates.Query.FirstOrDefault(x => x.DayState == "Complete");

            return daystate.Id;
        }
    }
}
