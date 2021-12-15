using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Exceptions;
using UI.Services.Interfaces;
using UI.Services.Models;
using System.Linq;

namespace UI.Components.AddClass
{
    public partial class AddClassComponent : ComponentBase
    {
        protected string value = String.Empty;
        protected string _errorMessage = String.Empty;
        protected string[] _errors;
        private bool error;
        private List<ClassModel> deserializedValue = new List<ClassModel>();
        private int teachersCount;
        private bool isBusy = false;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IClassHttpService ClassHttpService { get; set; }

        [Inject]
        public ITeacherHttpService TeacherHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isBusy = true;
            teachersCount = await TeacherHttpService.GetTeachersCount();
            if (teachersCount == 0)
            {
                ToastService.ShowError("Brak nauczycieli do wyboru");
                NavigationManager.NavigateTo("/");
                return;
            }
            isBusy = false;
            await LocalStorageService.RemoveItemAsync("MyClasses");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddClass");
            }
        }

        protected async Task HandleJson()
        {
            value = await LocalStorageService.GetItemAsync<string>("MyClasses");
            try
            {
                deserializedValue = JsonConvert.DeserializeObject<List<ClassModel>>(value);
            }
            catch (Exception ex)
            {
                error = true;
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
        }

        protected async Task HandleAddClass()
        {
            await HandleJson();
            if (error) { return; }
            error = await ComponentRequestHandler.HandleRequest<List<ClassModel>>
                (ClassHttpService.CreateClasses, deserializedValue, _errorMessage, _errors, ToastService);
            if (!error) { ToastService.ShowSuccess("Pomyślnie zapisano dane"); }
        }
    }
}