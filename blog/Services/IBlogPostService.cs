using blog.Models;
using blog.Models.Pagination;

namespace blog.Services
{
    public interface IBlogPostService
    {
        Task<int> CreateBlogPost(Post post);
        Task<PagedResponse<Post>> GetBlogPosts(PaginationParams queryParams);
    }
}
