using LawOf100.Features.Habits.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Habits
{
    public class GetTimeLine : PublicFeature<List<TimeLine>>
    {
        public GetTimeLine(IRepository<TimeLine> timelines)
        {
            TimeLines = timelines;
        }

        public IRepository<TimeLine> TimeLines { get; }

        public override async Task<List<TimeLine>> ExecuteAsync()
        {
            var timelines = await TimeLines.Query
                .Where(x => x.UserId == User.Id() && x.IsDeleted != true)
                .ToListAsync();

            return timelines;
        }

        
    }
}
