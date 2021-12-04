using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Responses;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Services.Services
{
    public class AuthenticationHttpService : IAuthenticationHttpService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationHttpService(NavigationManager navigationManager, IHttpService httpService
            , ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _navigationManager = navigationManager;
            _httpService = httpService;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Result<int>> RegisterUser(RegisterUserDto model)
        {
            return await _httpService.Post<Result<int>>("api/account/register", model);
        }

        public async Task<Result<string>> LoginUser(LoginUserDto model)
        {
            var result = await _httpService.Post<Result<string>>("api/account/login", model);
            if(result.Success)
            {
                await _localStorageService.SetItemAsStringAsync("access_token", result.Value);
                await _authenticationStateProvider.GetAuthenticationStateAsync();
            }
            return result;
        }


    }
}
