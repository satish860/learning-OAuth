using System.ComponentModel.DataAnnotations;

namespace Marven.idp.Entities
{
    public class UserClaims
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    }
}
