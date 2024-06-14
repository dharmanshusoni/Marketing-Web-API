using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Agent;
using Service.CompanyClient;

namespace marketing_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAgentInterface _agentService;

        public AgentController(ILogger<UserController> logger, IAgentInterface agentService)
        {
            _logger = logger;
            _agentService = agentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> add([FromBody] AgentDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _agentService.Insert(request);
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> get()
        {
            var response = await _agentService.getCard();
            return Ok(response);
        }

        [HttpGet("get/{agentId}")]
        public async Task<IActionResult> getById(int agentId)
        {
            var response = await _agentService.getById(agentId);
            return Ok(response);
        }

    }
}
