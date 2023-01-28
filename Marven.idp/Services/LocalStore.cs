using Marven.idp.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Marven.idp.Services
{
    public class LocalStore : ILocalStore
    {
        private readonly IMongoCollection<User> collection;

        public LocalStore(IMongoCollection<User> collection)
        {
            this.collection = collection;
        }


        public Task<User> FindBySubjectIdAsync(string subjectId)
        {
           return this.collection
                      .Find(p=>p.Subject==subjectId)
                      .FirstOrDefaultAsync();
        }

        public User FindByUsername(string username)
        {
            return this.collection
                      .Find(p => p.UserName == username)
                      .FirstOrDefault();
        }

        public async Task<ICollection<UserClaims>> GetClaimsBySubjectId(string subjectId)
        {
            var user = await FindBySubjectIdAsync(subjectId);
            return user.UserClaims;
        }

        public async Task<bool> IsUserActiveBySubjectId(string subjectId)
        {
            var user = await FindBySubjectIdAsync(subjectId);
            return user.Active;
        }

        public bool ValidateCredentials(string username, string password)
        { 
            var user = FindByUsername(username);

            if (user != null)
            {
                if (string.IsNullOrWhiteSpace(user.Password) && string.IsNullOrWhiteSpace(password))
                {
                    return true;
                }

                return user.Password.Equals(password);
            }

            return false;
        }
    }
}
