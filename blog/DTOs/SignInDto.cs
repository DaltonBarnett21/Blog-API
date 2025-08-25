using System.ComponentModel.DataAnnotations;

namespace blog.DTOs
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = string.Empty;
    }
}

