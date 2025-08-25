using blog.DTOs;
using blog.Models;
using blog.Models.Pagination;
using blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace blog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("blogPost")]
    public class BlogPostController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;
        private readonly IBlogPostService _blogPostService;
        public BlogPostController(ILogger<BlogPostController> logger, IBlogPostService blogPostService)
        {
            _logger = logger;
            _blogPostService = blogPostService;
        }


        /// <summary>
        /// saves a blog post to the db
        /// </summary>
        /// <response code="200">success message</response>
        /// <response code="204">No items found</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(string), 200)]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateBlogPost([FromBody] PostDTO request)
        {
            try
            {

                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var jsonString = JsonSerializer.Serialize(request.PostContent);

                var blogPost = new Post
                {
                    UserId = userId,
                    PostContent = jsonString,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                var rowsAffected = await _blogPostService.CreateBlogPost(blogPost);

                if (rowsAffected > 0)
                {
                    return Ok();
                }

                throw new Exception("There was an error creating the blog post");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error. Please try again later.");

            }
        }



        /// <summary>
        /// gets all blog posts
        /// </summary>
        /// <response code="200">success message</response>
        /// <response code="204">No items found</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(PagedResponse<Post>), 200)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetBlogPosts([FromQuery] PaginationParams queryParams)
        {
            try
            {
                var blogPosts = await _blogPostService.GetBlogPosts(queryParams);

                return Ok(blogPosts);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error. Please try again later.");

            }
        }




    }
}
