using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Users;

public class UpdateAccount : Feature<Account, string>
{

    public UpdateAccount(IRepository<Account> accounts)
    {
        Accounts = accounts;
    }

    public IRepository<Account> Accounts { get; }

    public override async Task<string> ExecuteAsync(Account request)
    {

            var account = await Accounts.FindAsync(User.Id());
            if (account == null || account.UserId != User.Id())
                throw new NotFoundException($"Account {request.Id} not found!");

            await Accounts.UpdateAsync(account);

            return account.Id;
        


    }
}

