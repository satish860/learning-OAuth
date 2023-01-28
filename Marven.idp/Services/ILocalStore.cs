using Duende.IdentityServer.Test;
using Marven.idp.Entities;

namespace Marven.idp.Services
{
    public interface ILocalStore
    {
        bool ValidateCredentials(string username, string password);

        User FindBySubjectId(string subjectId);

        User FindByUsername(string username);
    }
}
