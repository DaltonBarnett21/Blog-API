using Microsoft.AspNetCore.Identity;

namespace blog.Models
{
    public class User: IdentityUser
    {
    
        public ICollection<Post>? Posts { get; set; }

    }
}
