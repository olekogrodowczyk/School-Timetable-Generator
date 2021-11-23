using Application.Dto.RegisterUserVm;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI.Exceptions;
using UI.Interfaces;

namespace UI.Components.Authentication
{
    public partial class RegisterForm
    {
        private RegisterUserDto _model = new RegisterUserDto();
        private string _errorMessage = String.Empty;
        private string[] _errors;

        [Inject]
        public IAuthenticationHttpService authenticationHttpService { get; set; }

        [Inject]
        public IToastService toastService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task RegisterUser()
        {
            try
            {
                await authenticationHttpService.RegisterUser(_model);
                Navigation.NavigateTo("/login");
            }
            catch (ApiException e)
            {
                _errorMessage = e.ErrorResult.Message;
                _errors = e.Errors;
            }
            catch(Exception e)
            {
                _errorMessage = e.Message;
            }
            if(_errorMessage != String.Empty) { toastService.ShowError("", _errorMessage); }
            if(_errors.Length > 0)
            {
                foreach (string error in _errors)
                {
                    toastService.ShowError(error);
                }
            }
            if(_errorMessage == String.Empty) { toastService.ShowSuccess("Pomyślnie zarejestrowano");}
        }
    }
}
