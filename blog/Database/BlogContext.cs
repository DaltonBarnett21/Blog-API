using blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blog.Database
{
    public class BlogContext: IdentityDbContext<User>
    {

        public DbSet<User> Users {  get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options)
           : base(options)
        {
        }

    }
}
