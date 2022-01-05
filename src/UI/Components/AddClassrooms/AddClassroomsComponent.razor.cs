using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Shared.Dto.CreateClassroomDto;
using Shared.ViewModels;
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
        private bool error;
        private List<ClassroomModel> deserializedValue = new List<ClassroomModel>();
        private IEnumerable<ClassroomVm> ClassroomsCreated = new List<ClassroomVm>();

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IClassroomHttpService ClassroomHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public ITimetableStateHttpService TimetableStateHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LocalStorageService.RemoveItemAsync("MyClassrooms");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {   
            if (firstRender)
            {           
                await JSRuntime.InvokeVoidAsync("initializeAddClassrooms");
                ClassroomsCreated = await ClassroomHttpService.GetAllClassroomsCreated();
                StateHasChanged();
            }
        }
        
        
        protected async Task AddClassroom()
        {
            string classroomToAddString = await LocalStorageService.GetItemAsync<string>("ClassroomToAdd");
            ClassroomModel classroomToAdd = null;
            try
            {
                classroomToAdd = JsonConvert.DeserializeObject<ClassroomModel>(classroomToAddString);
            }
            catch (Exception ex)
            {
                error = true;
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
            if(error) { return; }
            error = await ComponentRequestHandler.HandleRequest<ClassroomModel>(ClassroomHttpService.CreateClassroom
                ,classroomToAdd, _errorMessage, _errors, ToastService);
            if (!error) 
            { 
                ToastService.ShowSuccess("Pomyślnie dodano nową salę");                                     
            }
            ClassroomsCreated = await ClassroomHttpService.GetAllClassroomsCreated();
            StateHasChanged();
        }

        protected async Task UpdateClassroom()
        {
            string classroomToEditString = await LocalStorageService.GetItemAsync<string>("ClassroomToEdit");
            ClassroomModel classroomToEdit = null;
            try
            {
                classroomToEdit = JsonConvert.DeserializeObject<ClassroomModel>(classroomToEditString);
            }
            catch (Exception ex)
            {
                error = true;
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
            error = await ComponentRequestHandler.HandleRequest<ClassroomModel>(ClassroomHttpService.UpdateClassroom
                , classroomToEdit, _errorMessage, _errors, ToastService);
            if (error) { NavigationManager.NavigateTo("addclassrooms", true); }
            if (!error)
            {
                ToastService.ShowSuccess("Pomyślnie zaktualizowano wybraną salę");             
            }
            ClassroomsCreated = await ClassroomHttpService.GetAllClassroomsCreated();
            StateHasChanged();
        }

        protected async Task DeleteClassroom(int classroomId)
        {
            error = await ComponentRequestHandler.HandleRequest<int>(ClassroomHttpService.DeleteClassroom
                , classroomId, _errorMessage, _errors, ToastService);
            if (!error)
            {
                ToastService.ShowSuccess("Pomyślnie usunięto wybraną salę");
            }
            ClassroomsCreated = await ClassroomHttpService.GetAllClassroomsCreated();
            StateHasChanged();
        }
    }
}