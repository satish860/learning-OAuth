using IdentityModel;
using Marven.idp.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Marven.idp
{
    public class SeedDatabase
    {
        private readonly IMongoCollection<User> collection;

        public SeedDatabase(IMongoCollection<User> collection)
        {
            this.collection = collection;
        }

        public bool IntializeDatabase()
        {
            var users = new List<User>()
            {
                new User
                {
                    Id = ObjectId.Parse("63d4bc641ce7882940913db8"),
                    Subject = "d860efca-22d9-47fd-8249-791ba61b07c7",
                    UserName = "David",
                    Password = "password",
                    Active = true,
                    UserClaims = new List<UserClaims>
                    {
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4bdfe2e371b9a5cbb4cff"),
                            Type = JwtClaimTypes.GivenName,
                            Value = "David",

                        },
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4be8c50447176ed765b2a"),
                            Type = JwtClaimTypes.FamilyName,
                            Value = "figg"
                        },
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4beb000fa2daf743a38c9"),
                            Type = "role",
                            Value = "payinguser"
                        },
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4bebe5932ad3c6cef1c95"),
                            Type = "country",
                            Value = "be"
                        }

                    }
                },

                new User
                {
                    Id = ObjectId.Parse("63d4cf9c16d5b9fd690ffc1f"),
                    Subject = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                    UserName = "Emma",
                    Password = "password",
                    Active = true,
                    UserClaims = new List<UserClaims>
                    {
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4cfac6726fee9c7b9af59"),
                            Type = JwtClaimTypes.GivenName,
                            Value = "Emma",

                        },
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4cfb8c848b4e3a0b30cb5"),
                            Type = JwtClaimTypes.FamilyName,
                            Value = "figg"
                        },
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4cfc65a5b0be6e48422bb"),
                            Type = "role",
                            Value = "freeuser"
                        },
                        new UserClaims
                        {
                            Id = ObjectId.Parse("63d4cfd1cf1cd635f76d208d"),
                            Type = "country",
                            Value = "nl"
                        }

                    }
                }
            };
            foreach (var item in users)
            {
                var filter = Builders<User>.Filter.Where(p => p.Id == item.Id);
                this.collection.ReplaceOne(filter,item,new ReplaceOptions
                {
                    IsUpsert = true
                });
            }
            
            return true;
        }
    }
}
