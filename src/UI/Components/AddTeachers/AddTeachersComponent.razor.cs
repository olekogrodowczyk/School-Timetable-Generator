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

namespace UI.Components.AddTeachers
{
    public partial class AddTeachersComponent
    {
        protected string value = String.Empty;
        protected string _errorMessage = String.Empty;
        protected string[] _errors;
        private List<TeacherModel> deserializedValue = new List<TeacherModel>();

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public ITeacherHttpService TeacherHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LocalStorageService.RemoveItemAsync("MyTeachers");
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddTeachers");
            }
        }

        protected async Task HandleJson()
        {
            value = await LocalStorageService.GetItemAsync<string>("MyTeachers");
            Console.WriteLine(value);
            try
            {
                deserializedValue = JsonConvert.DeserializeObject<List<TeacherModel>>(value);
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
        }
        protected async Task HandleAddClass()
        {
            await HandleJson();
            await ComponentRequestHandler.HandleRequest<List<TeacherModel>>
                (TeacherHttpService.CreateTeachersWithStudents, deserializedValue, _errorMessage, _errors, ToastService);
            if (_errorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie zapisano dane"); }
        }
    }
}
