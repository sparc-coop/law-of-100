using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Users;

public record CreateAccountRequest(string Name);
public class CreateAccount : Feature<CreateAccountRequest, string>
{
    public CreateAccount(IRepository<Account> accounts)
    {
        Accounts = accounts;
    }

    public IRepository<Account> Accounts { get; }

    public override async Task<string> ExecuteAsync(CreateAccountRequest request)
    {
        var account = new Account(User.Id(), request.Name);
        await Accounts.AddAsync(account);

        return account.Id;
    }
}
