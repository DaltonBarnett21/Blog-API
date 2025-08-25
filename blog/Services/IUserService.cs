using blog.Models;
using Microsoft.AspNetCore.Identity;

namespace blog.Providers
{
    public interface IUserService
    {
       Task<User> GetUserByUserName(string username); 

       Task<IdentityResult> CreateUser(User user, string password);

       Task<SignInResult> SignInUser(string username, string password);
    }
}
