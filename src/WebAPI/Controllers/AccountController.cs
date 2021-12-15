using Application.Interfaces;
using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared.Responses;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;

        public AccountController(IIdentityService identityService, IUserService userService)
        {
            _identityService = identityService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var result = await _identityService.RegisterAsync(model);
            return Ok(new OkResult<int>(result, "Pomyślnie zarejestrowano użytkownika"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            var result = await _identityService.LoginAsync(model);
            return Ok(new OkResult<string>(result, "Pomyślnie zalogowano"));
        }

        [Authorize]
        [HttpGet("getcurrenttimetable")]
        public async Task<IActionResult> GetCurrentTimetable()
        {
            int result = await _userService.GetCurrentActiveTimetable();
            return Ok(new OkResult<int>(result, "Pomyślnie zwrócono aktywny plan lekcji"));
        }
    }
}