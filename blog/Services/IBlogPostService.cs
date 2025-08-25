using blog.Models;

namespace blog.Services
{
    public interface IBlogPostService
    {
        Task<int> CreateBlogPost(Post post);
    }
}
