using blog.Database;
using blog.Models;
using blog.Models.Pagination;
using blog.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagedResponse<Post>> GetBlogPosts(PaginationParams queryParams)
        {
            var query = _context.Posts.AsQueryable();

            // Implement later
            //if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            //{
            //    query = query.Where(i => i.Name.Contains(queryParams.SearchTerm));
            //}

            //if (!string.IsNullOrEmpty(queryParams.Category))
            //{
            //    query = query.Where(i => i.Category == queryParams.Category);
            //}
            var totalRecords = await query.CountAsync();

            var posts = await query.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();

            var pagedResponse = new PagedResponse<Post>(posts, queryParams.PageNumber, queryParams.PageSize, totalRecords);

            return pagedResponse;
        }
    }
}
