using System.ComponentModel.DataAnnotations;

namespace Marven.idp.Entities
{
    public class User
    {
       
        public Guid Id { get; set; }

       
        public string Subject { get; set; }

       
        public string UserName { get; set; }

       
        public string Password { get; set; }

        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
