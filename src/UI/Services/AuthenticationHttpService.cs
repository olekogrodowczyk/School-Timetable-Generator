using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Application.Responses;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class AuthenticationHttpService : IAuthenticationHttpService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IHttpService _httpService;

        public AuthenticationHttpService(NavigationManager navigationManager, IHttpService httpService)
        {
            _navigationManager = navigationManager;
            _httpService = httpService;
        }

        public async Task<Result<int>> RegisterUser(RegisterUserDto model)
        {
            return await _httpService.Post<Result<int>>("api/account/register", model);
        }

        public async Task<Result<string>> LoginUser(LoginUserDto model)
        {
            return await _httpService.Post<Result<string>>("api/account/login", model);
        }


    }
}
