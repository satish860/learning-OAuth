using Duende.IdentityServer.Test;
using Marven.idp.Entities;

namespace Marven.idp.Services
{
    public interface ILocalStore
    {
        bool ValidateCredentials(string username, string password);

        Task<User> FindBySubjectIdAsync(string subjectId);

        User FindByUsername(string username);

        Task<ICollection<UserClaims>> GetClaimsBySubjectId(string subjectId);

        Task<bool> IsUserActiveBySubjectId(string subjectId);
    }
}
