using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Dto.CreateTimetableDto;
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
        private readonly ITimetableHttpService _timetableHttpService;

        public AuthenticationHttpService(NavigationManager navigationManager, IHttpService httpService
            , ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider
            , ITimetableHttpService timetableHttpService)
        {
            _navigationManager = navigationManager;
            _httpService = httpService;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
            _timetableHttpService = timetableHttpService;
        }

        public async Task RegisterUser(RegisterUserDto model)
        {
            var registerResult = await _httpService.Post<OkResult<int>>("api/account/register", model);
        }

        public async Task LoginUser(LoginUserDto model)
        {
            var result = await _httpService.Post<OkResult<string>>("api/account/login", model);
            if (result.Success)
            {
                await _localStorageService.SetItemAsStringAsync("access_token", result.Value);
                await _authenticationStateProvider.GetAuthenticationStateAsync();
            }
        }
    }
}