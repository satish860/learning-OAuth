using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using System.Security.Claims;

namespace Marven.idp.Services
{
    public class LocalProfileuserService : IProfileService
    {
        private readonly ILocalStore localStore;

        public LocalProfileuserService(ILocalStore localStore)
        {
            this.localStore = localStore;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var SubjectId = context.Subject.GetSubjectId();
            var userClaims = await this.localStore.GetClaimsBySubjectId(SubjectId);
            context.AddRequestedClaims(
                userClaims
                .Select(p => new Claim(p.Type, p.Value)));
            
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var SubjectId = context.Subject.GetSubjectId();
            await this.localStore.IsUserActiveBySubjectId(SubjectId);
        }
    }
}
