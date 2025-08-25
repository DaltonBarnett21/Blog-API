using blog.Models;
using Microsoft.AspNetCore.Identity;

namespace blog.Providers
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        public Task<IdentityResult> CreateUser(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<User> GetUserByUserName(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<SignInResult> SignInUser(string username, string password)
        {
            return _signInManager.PasswordSignInAsync(username, password, isPersistent: true, lockoutOnFailure: true);
        }
    }
}
