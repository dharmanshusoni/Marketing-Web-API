using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Candidate;
using Service.CompanyClient;
using Service.Master;
using System.Data;

namespace marketing_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CandidateController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ICandidateInterface _candidateService;
        
        public CandidateController(ILogger<UserController> logger, ICandidateInterface candidateService)
        {
            _logger = logger;
            _candidateService = candidateService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> add([FromBody] CandidateDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _candidateService.Insert(request);
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> get()
        {
            var response = await _candidateService.getCard();
            return Ok(response);
        }

        [HttpGet("get/{candidateId}")]
        public async Task<IActionResult> getById(int candidateId)
        {
            var response = await _candidateService.getById(candidateId);
            return Ok(response);
        }

    }
}
