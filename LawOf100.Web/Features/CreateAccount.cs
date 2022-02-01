using LawOf100.Features.Features.Entities;
using Sparc.Features;

namespace LawOf100.Features.Features
{
    //public record CreateAccountResponse(int Account);
    public class CreateAccount : PublicFeature<int, string>
    {
        public override async Task<string> ExecuteAsync(int userId)
        {
            var account = new Account("John", userId);

            return account.Id;
        }
    }
}
