using Sparc.Core;

namespace LawOf100.Features.Features.Entities
{
    public class Account : Root<string>
    {
        private Account()
        {
            Id = Guid.NewGuid().ToString();
            Name = "John";
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

        public Account(string name, int? userId = null) : this()
        {
            Name = name;
            UserId = userId;
        }

        public string AccountId { get; set; }
        public string Name { get; set;  }
        public int? UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
