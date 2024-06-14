using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.CompanyClient;
using Service.Master;
using System.Data;

namespace marketing_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyclientController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ICompanyclientInterface _companyclientService;
        
        public CompanyclientController(ILogger<UserController> logger, ICompanyclientInterface companyclientService)
        {
            _logger = logger;
            _companyclientService = companyclientService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> add([FromBody] CompanyClientDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _companyclientService.Insert(request);
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> get()
        {
            var response = await _companyclientService.getCard();
            return Ok(response);
        }

        [HttpGet("get/{companyClientId}")]
        public async Task<IActionResult> getById(int companyClientId)
        {
            var response = await _companyclientService.getById(companyClientId);
            return Ok(response);
        }

    }
}
