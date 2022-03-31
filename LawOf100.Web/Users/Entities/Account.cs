using Sparc.Core;

namespace LawOf100.Features.Users.Entities;

public class Account : Root<string>
{
    private Account()
    {
        Id = "";
        Name = "";
        Nickname = "";
        ShortIntro = "";
        DateCreated = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;
    }

    public Account(string id) : this()
    {
        Id = id;
    }

    public Account(string id, string name, string? intro, string nickname) : this(id)
    {
        Name = name;
        Nickname = nickname;
        ShortIntro = intro;
    }

    public string Name { get; set; }
    public string Nickname { get; set; }
    public string? ShortIntro { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public bool IsProfileSet => !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(Nickname);

    internal void Update(string name, string nickname, string? shortIntro)
    {
        Name = name;
        Nickname = nickname;
        ShortIntro = shortIntro;
        DateModified = DateTime.UtcNow;
    }

    internal void SetCurrentHabit(string habitId)
    {
        CurrentHabitId = habitId;
    }

    public string? CurrentHabitId { get; set; }
    public string UserId { get { return Id; } set { Id = value; } }
}