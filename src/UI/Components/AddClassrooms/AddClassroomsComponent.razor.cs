using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI.Services.Models;

namespace UI.Components.AddClassrooms
{
    public partial class AddClassroomsComponent
    {
        protected string value = String.Empty;
        protected string _errorMessage = String.Empty;
        protected string[] _errors;
        private List<ClassroomModel> deserializedValue = new List<ClassroomModel>();

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IClassroomHttpService ClassroomHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LocalStorageService.RemoveItemAsync("MyClassrooms");
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddClassrooms");
            }
        }

        protected async Task HandleJson()
        {
            value = await LocalStorageService.GetItemAsync<string>("MyClassrooms");
            Console.WriteLine(value);
            try
            {
                deserializedValue = JsonConvert.DeserializeObject<List<ClassroomModel>>(value);
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
        }

        protected async Task HandleAddClassrooms()
        {
            await HandleJson();
            await ComponentRequestHandler.HandleRequest<List<ClassroomModel>>
                (ClassroomHttpService.CreateClassrooms, deserializedValue, _errorMessage, _errors, ToastService);
            if (_errorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie zapisano dane"); }
        }
    }
}
