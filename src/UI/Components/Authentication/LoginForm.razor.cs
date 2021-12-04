using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using UI.Services.Exceptions;
using UI.Services.Interfaces;

namespace UI.Components.Authentication
{
    public partial class LoginForm
    {
        private LoginUserDto _model = new LoginUserDto();
        private string _errorMessage = String.Empty;
        private string[] _errors;

        [Inject]
        public IAuthenticationHttpService AuthenticationHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task LoginUser()
        {
            try
            {
                await AuthenticationHttpService.LoginUser(_model);
            }
            catch (ApiException e)
            {
                _errorMessage = e.ErrorResult.Message;
                _errors = e.ErrorResult.Errors;
            }
            catch (Exception e)
            {
                _errorMessage = e.Message;
            }
            if (_errorMessage != String.Empty) { ToastService.ShowError(String.Empty, _errorMessage); }
            if (_errors != null)
            {
                foreach (string error in _errors)
                {
                    ToastService.ShowError(error);
                }
            }
            if (_errorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie zalogowano"); }
            Navigation.NavigateTo("/");
        }
    }
}
