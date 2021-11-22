﻿using Application.Dto.RegisterUserVm;
using Application.Responses;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class AuthenticationHttpService : IAuthenticationHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly HttpService _httpService;

        public AuthenticationHttpService(HttpClient client, NavigationManager navigationManager, HttpService httpService)
        {
            _httpClient = client;
            _navigationManager = navigationManager;
            _httpService = httpService;
        }

        public async Task<Result<int>> RegisterUserAsync(RegisterUserDto model)
        {
            return await _httpService.Post<Result<int>>("api/account/register", model);
        }


    }
}
