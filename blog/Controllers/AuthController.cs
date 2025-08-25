using blog.DTOs;
using blog.Models;
using blog.Providers;
using blog.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public AuthController(ILogger<AuthController> logger, IUserService userService, ITokenHelper tokenHelper)
    {
        _logger = logger;
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Registers a user to have access to the app
    /// </summary>
    /// <response code="200">success message</response>
    /// <response code="204">No items found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Internal server error</response>
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] RegisterDto request)
    {
        try
        {
      
            var foundUser = _userService.GetUserByUserName(request.UserName);

            if (foundUser.Result is not null)
            {
                return Conflict("user already exists");
            }

            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = request.Password
            };

            var userCreation = _userService.CreateUser(newUser, newUser.PasswordHash);

            if (userCreation.Result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            
            return BadRequest(userCreation.Result.Errors.ToArray());
        
           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, "Internal server error. Please try again later.");

        }
    }



    /// <summary>
    /// Registers a user to have access to the app
    /// </summary>
    /// <response code="200">success message</response>
    /// <response code="204">No items found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Internal server error</response>
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost]
    [Route("signin")]
    public IActionResult SignIn([FromBody] SignInDto request)
    {
        try
        {
            
            var signIn = _userService.SignInUser(request.UserName, request.Password);

            if (signIn.Result.Succeeded)
            {
                var user = _userService.GetUserByUserName(request.UserName);
                var token = _tokenHelper.GenerateJwtToken(user.Result.Id);

                return Ok(new {token});
            }

            return Unauthorized();


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, "Internal server error. Please try again later.");

        }
    }
}
