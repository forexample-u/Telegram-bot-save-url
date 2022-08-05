using Microsoft.AspNetCore.Identity;

namespace WebApplicationBooks.Models
{
    public class AccountUser : IdentityUser
    {
        public string MessagerUsername { get; set; }
    }
}