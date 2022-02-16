using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Users;

public class GetAccount : Feature<Account>
{

    public GetAccount(IRepository<Account> accounts)
    {
        Accounts = accounts;
    }

    public IRepository<Account> Accounts { get; }

    public override async Task<Account> ExecuteAsync()
    {
        var account = await Accounts.FindAsync(User.Id());
        if (account == null)
            throw new NotFoundException($"Account not found!");

        return account;
    }
}

