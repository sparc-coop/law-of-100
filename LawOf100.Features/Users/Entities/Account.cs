using Sparc.Core;

namespace LawOf100.Features.Users.Entities;

public class Account : Root<string>
{
    private Account()
    {
        Id = Guid.NewGuid().ToString();
        Name = "";
        DateCreated = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;
    }

    public Account(string id, string name) : this()
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public string UserId { get { return Id; } set { Id = value; } }
}
