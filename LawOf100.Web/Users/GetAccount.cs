using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Users;

public class GetAccount : PublicFeature<string, Account>
{

    public GetAccount(IRepository<Account> accounts)
    {
        Accounts = accounts;
    }

    public IRepository<Account> Accounts { get; }

    public override async Task<Account> ExecuteAsync(string id)
    {
        var account = await Accounts.FindAsync(id);
        if (account == null)
            throw new NotFoundException($"Account {id} not found!");

        return account;
    }
}

