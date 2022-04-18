using Sparc.Core;
using Sparc.Kernel;
using Sparc.Notifications.Azure;

namespace LawOf100.Users;

public class GetAccount : Feature<Device, Account>
{
    public GetAccount(IRepository<Account> accounts, AzureNotificationService notifications)
    {
        Accounts = accounts;
        Notifications = notifications;
    }

    public IRepository<Account> Accounts { get; }
    public AzureNotificationService Notifications { get; }

    public override async Task<Account> ExecuteAsync(Device device)
    {
        var account = await Accounts.FindAsync(User.Id());
        if (account == null)
        {
            account = new Account(User.Id());
            await Accounts.AddAsync(account);
        }

        account.RegisterDevice(device);

        if (device.Id != null && device.PushToken != null)
            await Notifications.RegisterAsync(User.Id(), device);

        await Accounts.UpdateAsync(account);

        return account;
    }
}

