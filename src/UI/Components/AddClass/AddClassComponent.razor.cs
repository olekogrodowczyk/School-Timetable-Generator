using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Exceptions;
using UI.Services.Interfaces;
using UI.Services.Models;

namespace UI.Components.AddClass
{
    public partial class AddClassComponent
    {
        protected string value = String.Empty;
        private string _errorMessage = String.Empty;
        private string[] _errors;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public IClassHttpService ClassHttpService { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddClass");
            }
        }

        protected async Task HandleAddClass()
        {
            value = await LocalStorageService.GetItemAsync<string>("MyClasses");
            List<ClassModel> deserializedValue = new List<ClassModel>();
            try
            {
                deserializedValue = JsonConvert.DeserializeObject<List<ClassModel>>(value);
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }

            try
            {
                await ClassHttpService.CreateClass(deserializedValue);
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
            if (_errorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie zapisano dane"); }

            
        }
    }
}
