namespace blog.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string PostContent { get; set; } = string.Empty;

        public string PostStatus { get; set; }  = "Active";

        public string CommentsStatus { get; set; } = "Active";

        public string? PostCategory { get; set; }

        public DateTime CreatedAt { get; set; } 

        public DateTime UpdatedAt { get; set; }


        public string? UserId { get; set; }

        public User? Author { get; set; }


    }
}
