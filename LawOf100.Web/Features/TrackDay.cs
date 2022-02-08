using LawOf100.Features.Features.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Features
{
    public class TrackDay : PublicFeature<int, string>
    {
        public TrackDay(IRepository<Progression> days)
        {
            Days = days;
        }

        public IRepository<Progression> Days { get; }

        public override async Task<string> ExecuteAsync(int currentday)
        {
            var day = new Progression (currentday);
            await Days.AddAsync(day);

            day = Days.Query.FirstOrDefault(x => x.Day == 1 /*currentday*/);

            return day.Id;
        }
    }
}
