using blog.Database;
using blog.Models;
using blog.Services;

namespace blog.Providers
{
    public class BlogPostService: IBlogPostService
    {
        private readonly BlogContext _context;

        public BlogPostService(BlogContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBlogPost(Post post)
        {
            _context.Posts.Add(post);

            var rowsEffected =  await _context.SaveChangesAsync();

            return rowsEffected;
        }
    }
}
