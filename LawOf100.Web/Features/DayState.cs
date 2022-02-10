using LawOf100.Features.Features.Entities;
using Sparc.Core;
using Sparc.Features;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LawOf100.Features.Features
{
    public class DayState : PublicFeature<string, List<Progression>>
    {
        
        public DayState(IRepository<Progression> daystates)
        {
            DayStates = daystates;
        }

        public IRepository<Progression> DayStates { get; }

        public override async Task<List<Progression>> ExecuteAsync(string currentstate)
        {
            var daystates = await DayStates.Query
                .Where(x => x.DayState == currentstate)
                .ToListAsync();

            return daystates;


            //var daystate = new Progression(currentstate);
            //await DayStates.AddAsync(daystate);

            //daystate = DayStates.Query.FirstOrDefault(x => x.DayState == "Complete");

            //return daystate.Id;
        }
    }
}
