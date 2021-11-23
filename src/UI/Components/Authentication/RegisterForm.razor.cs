using Application.Dto.RegisterUserVm;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Exceptions;
using UI.Interfaces;

namespace UI.Components.Authentication
{
    public partial class RegisterForm
    {
        private RegisterUserDto _model = new RegisterUserDto();
        private string _error = String.Empty;

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
                _error = e.ErrorResult.Message;
            }
            catch(Exception e)
            {
                _error = e.Message;
            }
            if(_error!=String.Empty) { toastService.ShowError(_error); }
            else { toastService.ShowSuccess("Pomyślnie zarejestrowano");}
        }
    }
}
