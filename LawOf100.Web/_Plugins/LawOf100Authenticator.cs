using IdentityServer4.Services;
using Sparc.Authentication.SelfHosted;
using System.Security.Claims;

namespace LawOf100.Features._Plugins
{
    public class LawOf100Authenticator : SparcAuthenticator
    {
        public LawOf100Authenticator(IIdentityServerInteractionService interaction, IHttpContextAccessor httpContextAccessor) : base(interaction, httpContextAccessor)
        {
        }

        public override Task<List<Claim>> GetClaimsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public override string? GetUserId(ClaimsPrincipal? principal)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> IsActiveAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> LoginAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
