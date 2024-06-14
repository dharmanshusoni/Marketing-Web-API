using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Service.JwtAuthManager;
using Service.User;
using Service.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace marketing_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInterface _userService;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AuthenticateController(
            IConfiguration configuration,
            IUserInterface userRepository,
            ILogger<AuthenticateController> logger,
            IJwtAuthManager jwtAuthManager)
        {
            _configuration = configuration;
            _userService = userRepository;
            _logger = logger;
            _jwtAuthManager = jwtAuthManager;
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> getToken([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_userService.IsValidUserCredentials(request.UserName, request.Password))
            {
                return Unauthorized(
                    new LoginResult
                    {
                        UserName = request.UserName,
                        Role = "",
                        Status = 0,
                        Message = "unauthorized",
                        AccessToken = "",
                        RefreshToken = ""
                    });
            }

            var role = _userService.GetUserRole(request.UserName);
            var userObject = _userService.getUser(request.UserName, request.Password);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier,userObject.UserId.ToString())
            };

            var jwtResult = await _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{request.UserName}] logged in the system.");
            return Ok(new LoginResult
            {
                UserName = request.UserName,
                Role = role,
                Status = 1,
                Message = "success",
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        [HttpGet("user")]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            //throw new ApplicationException("Invalid Token");
            return Ok(new LoginResult
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                UserName = User.Identity?.Name,
                Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            // optionally "revoke" JWT token on the server side --> add the current token to a block-list
            // https://github.com/auth0/node-jsonwebtoken/issues/375

            var userName = User.Identity?.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            _logger.LogInformation($"User [{userName}] logged out the system.");
            return Ok();
        }

        [HttpPost("refresh-token")]
        //[Authorize]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity?.Name;
                _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = await _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    Status = 1,
                    Message = "success",
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("impersonation")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<ActionResult> Impersonate([FromBody] ImpersonationRequest request)
        {
            var userName = User.Identity?.Name;
            _logger.LogInformation($"User [{userName}] is trying to impersonate [{request.UserName}].");

            var impersonatedRole = _userService.GetUserRole(request.UserName);
            if (string.IsNullOrWhiteSpace(impersonatedRole))
            {
                _logger.LogInformation($"User [{userName}] failed to impersonate [{request.UserName}] due to the target user not found.");
                return BadRequest($"The target user [{request.UserName}] is not found.");
            }
            if (impersonatedRole == UserRoles.ADMIN)
            {
                _logger.LogInformation($"User [{userName}] is not allowed to impersonate another Admin.");
                return BadRequest("This action is not supported.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName),
                new Claim(ClaimTypes.Role, impersonatedRole),
                new Claim("OriginalUserName", userName ?? string.Empty)
            };

            var jwtResult = await _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{request.UserName}] is impersonating [{request.UserName}] in the system.");
            return Ok(new LoginResult
            {
                UserName = request.UserName,
                Role = impersonatedRole,
                OriginalUserName = userName,
                Status = 1,
                Message = "success",
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        [HttpPost("stop-impersonation")]
        public async Task<ActionResult> StopImpersonation()
        {
            var userName = User.Identity?.Name;
            var originalUserName = User.FindFirst("OriginalUserName")?.Value;
            if (string.IsNullOrWhiteSpace(originalUserName))
            {
                return BadRequest("You are not impersonating anyone.");
            }
            _logger.LogInformation($"User [{originalUserName}] is trying to stop impersonate [{userName}].");

            var role = _userService.GetUserRole(originalUserName);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,originalUserName),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtResult = await _jwtAuthManager.GenerateTokens(originalUserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{originalUserName}] has stopped impersonation.");
            return Ok(new LoginResult
            {
                UserName = originalUserName,
                Role = role,
                OriginalUserName = null,
                Status = 1,
                Message = "success",
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }
    }
}
