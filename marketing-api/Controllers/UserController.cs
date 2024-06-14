using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Service.User;
using Service.Master;
using System.Security.Claims;

namespace marketing_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserInterface _userService;
        
        public UserController(ILogger<UserController> logger, IUserInterface userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var userName = User.Identity?.Name;
            var identity = User.Identity;
            _logger.LogInformation($"User [{userName}] is viewing values with Identity "+identity);
            return new[] { "value1", "value2" };
        }

        //[EnableCors()]
        [HttpPost]
        public Object Login([FromBody] UserDTO user)
        {
            if (user.UserEmailId == "")
            {
                return JsonConvert.SerializeObject(new Result { message = "Insert Username" });
            }
            else
            {
                return _userService.Login(user);
            }
        }

        
    }
}
