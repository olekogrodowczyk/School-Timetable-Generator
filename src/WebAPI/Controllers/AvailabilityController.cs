using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateAvailabilityDto;
using Shared.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAvailabilityDto model)
        {
            int result = await _availabilityService.CreateAvailability(model);
            return Ok(new OkResult<int>(result, "Pomyślnie dodano nową dostępność"));
        }
    }
}