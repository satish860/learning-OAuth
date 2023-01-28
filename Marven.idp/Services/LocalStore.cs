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


        public User FindBySubjectId(string subjectId)
        {
           return this.collection
                      .Find(p=>p.Subject==subjectId)
                      .FirstOrDefault();
        }

        public User FindByUsername(string username)
        {
            return this.collection
                      .Find(p => p.UserName == username)
                      .FirstOrDefault();
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
