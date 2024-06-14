using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Master;
using Service.User;

namespace marketing_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMasterInterface _masterService;
        
        public MasterController(ILogger<UserController> logger, IMasterInterface masterService)
        {
            _logger = logger;
            _masterService = masterService;
        }

        [HttpGet("country")]
        public async Task<ActionResult<CountryDTO>> country()
        {
            return Ok(await _masterService.getCountry());
        }

        [HttpGet("state")]
        public async Task<ActionResult<StateDTO>> state(int countryId)
        {
            return Ok(await _masterService.getState(countryId));
        }

        [HttpGet("city")]
        public async Task<ActionResult<CityDTO>> city(int stateId)
        {
            return Ok(await _masterService.getCity(stateId));
        }

        [HttpGet("gender")]
        public async Task<ActionResult<GenderDTO>> gender(int stateId)
        {
            return Ok(await _masterService.getGender(stateId));
        }

        [HttpGet("language")]
        public async Task<ActionResult<LanguageDTO>> language(int stateId)
        {
            return Ok(await _masterService.getLanguage(stateId));
        }

        [HttpGet("qualification")]
        public async Task<ActionResult<QualificationDTO>> qualification(int qualificationId)
        {
            return Ok(await _masterService.getQualification(qualificationId));
        }

        [HttpGet("skill")]
        public async Task<ActionResult<SkillDTO>> skill(int skillId)
        {
            return Ok(await _masterService.getSkill(skillId));
        }
    }
}
