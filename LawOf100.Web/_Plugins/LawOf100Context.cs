using Microsoft.EntityFrameworkCore;

namespace LawOf100.Features._Plugins
{
    public class LawOf100Context : DbContext
    {
        public LawOf100Context(DbContextOptions options) : base(options)
        {
        }
    }
}
