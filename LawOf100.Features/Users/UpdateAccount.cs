using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;

namespace LawOf100.Features.Users;

public record UpdateAccountRequest(string Name, string? ShortIntro, string Nickname);
public class UpdateAccount : Feature<UpdateAccountRequest, Account>
{
    public UpdateAccount(IRepository<Account> accounts)
    {
        Accounts = accounts;
    }

    public IRepository<Account> Accounts { get; }

    public override async Task<Account> ExecuteAsync(UpdateAccountRequest request)
    {
        // Check for duplicate nicknames
        var otherAccountsWithNickname = Accounts.Query.Where(x => x.Nickname == request.Nickname && x.Id != User.Id()).Count();
        if (otherAccountsWithNickname > 0)
            throw new Exception("Unfortunately, someone else is already using that nickname.");
        
        var account = await Accounts.FindAsync(User.Id());
        if (account == null)
        {
            account = new Account(User.Id(), request.Name, request.ShortIntro, request.Nickname);
            await Accounts.AddAsync(account);
        }
        else
        {
            await Accounts.ExecuteAsync(account, a => a.Update(request.Name, request.Nickname, request.ShortIntro));
        }

        return account;
    }
}
