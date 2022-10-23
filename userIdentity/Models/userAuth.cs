using Microsoft.AspNetCore.Identity;

namespace userIdentity.Models
{
    public class userAuth : IdentityUser
    {
        public string Name { get; set; }
    }
}
